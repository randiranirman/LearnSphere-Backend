using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserManagement.Application.Dtos;
using UserManagement.Application.Repositories;
using UserManagement.Domain.Domain;
using UserManagement.Domain.Repositories;
using UserManagement.Infrastructure.Data;


namespace UserManagement.Infrastructure.Services
{
    public class AuthService(UserDbContext context, IConfiguration configuration, IEmailService emailService) : IAuthService
    {
        public async Task<bool> ChangeCredentials(string username, ChangeCredentialsDto request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user is null)
            {
                return false;
            }
            var hasher = new PasswordHasher<User>();
            if (hasher.VerifyHashedPassword(user, user.hashedPassword, request.TemporaryPassword) == PasswordVerificationResult.Failed)
            {
                return false;


            }
            if (request.NewPassword != request.ConfirmPassword)
            {
                return false;
            }
            user.hashedPassword = hasher.HashPassword(user, request.NewPassword);
            user.IsFirstLogin = false;
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return true;

        }

        // firstlogin attempt functionality

        public async Task<bool> IsFirstLogin(UserDto request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user is null)
            {
                return false;
            }

            var firstLoginAttempt = user.IsFirstLogin;

            if (firstLoginAttempt)
            {
                return true;


            }
            else
            {
                return false;
            }

        }



        // user log in functionality 

        public async Task<TokenResponseDto?> LoginAsync(UserDto request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user is null)
            {
                Console.WriteLine("this is line got executed");
                return null;
            }

            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.hashedPassword, request.Password) == PasswordVerificationResult.Failed)
            {
                // returning the access token and the refresh token to the response  
                return null;
            }

            TokenResponseDto response = await createTokenResponse(user);
            return response;
        }

        private async Task<TokenResponseDto> createTokenResponse(User user)
        {
            if (user is null)
            {
                return null;
            }
            return new TokenResponseDto
            {
                AccessToken = createToken(user),
                RefreshToken = await GenerateAndSaveRefreshToken(user)


            };

        }

        private async Task<string> GenereateAndSaveRefreshToken(User user)
        {
            var refreshToken = GenerateRefershToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            context.Users.Update(user); // Explicitly update the entity
            await context.SaveChangesAsync();

            return refreshToken;
        }

        private string createToken(User user)
        {
            var claims = new List<Claim>
            {

                new Claim( ClaimTypes.Name, user.Username),
                new Claim( ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("isFirstLogin", user.IsFirstLogin.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds


                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);


        }
        // generate refresh token 
        private String GenerateRefershToken()
        {
            var randomNumber = new Byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        // generate and save refresh token in the database
        private async Task<string> GenerateAndSaveRefreshToken(User user)
        {
            var refreshToken = GenerateRefershToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            context.Users.Update(user);// explicitly update the user 
            await context.SaveChangesAsync();
            // return the refresh token

            return refreshToken;

        }


        // log out functionality 
        public async Task<bool> LogoutAsync(string username)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user is null)
            {
                return false;
            }
            // setting the refresh token null when user logging out 
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
            if (user is null)
            {
                return null;

            }


            return await createTokenResponse(user);


        }

        private async Task<User> ValidateRefreshTokenAsync(int userId, string refreshToken)
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.Id == userId && u.RefreshToken == refreshToken);
            if (user is null)
            {
                return null;

            }
            return user;

        }

        public async Task<UserDto?> RegisterAsync(UserDto request)
        {


            if (await context.Users.AnyAsync(u => u.Username == request.Username))
            {
                return null;
            }
            request.Password = GenaratERandomPassword();    
            var user = new User();
            var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);
            user.IsFirstLogin = true;
            user.Username = request.Username;
            user.hashedPassword = hashedPassword;
            user.Email = request.Email;
            user.Name = request.Name;
            user.Role = request.Role;

            context.Users.Add(user);
            await context.SaveChangesAsync();
            // Check the role and create the corresponding entity
            if (request.Role.ToLower() == "admin")
            {

                var admin = new Admin()
                {
                    Id = user.Id,
                    FirstName = user.Name

                };
                context.Entry(admin).State = EntityState.Modified;
                context.Admins.Add(admin);
                await context.SaveChangesAsync();


            }
            else if (request.Role.ToLower() == "teacher")
            {
                var teacher = new Teacher()
                {
                    Id = user.Id,
                    FirstName = user.Name,
                };

                context.Entry(teacher).State = EntityState.Modified;

            }
            else if (request.Role.ToLower() == "student")
            {
                var student = new Student()
                {
                    Id = user.Id,
                    FirstName = user.Name,
                };

            }
           // sending the email to the particular user
            await emailService.SendEmailToUserAsync(user.Email, EmailTemplates.WelcomeSubject, EmailTemplates.WelcomeBody(user.Name, user.Username, request.Password));




            return new UserDto
            {
                Username = request.Username,
                Name = request.Name,
                Role = request.Role,
                Email = request.Email

                    
            };

        }

        public async Task<bool> UpdateFirstLoginStatus(UserDto request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user is null)
            {
                return false;
            }
            user.IsFirstLogin = false;


            context.Users.Update(user);
            await context.SaveChangesAsync();
            return true;

        }

        public String GenaratERandomPassword(int length=6)
        {

            // create a string of characters  , numbres ,  and special character
            string  validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();
            // select one random character from  the string 
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }

            return new string(chars);




        }

    }
       
}
