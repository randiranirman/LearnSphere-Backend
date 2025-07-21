using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CsvHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using UserManagement.Application.Dtos;
using UserManagement.Application.Repositories;
using UserManagement.Domain.Domain;
using UserManagement.Domain.Repositories;
using UserManagement.Infrastructure.Data;
using Microsoft.Extensions.Caching.Memory;

namespace UserManagement.Infrastructure.Services
{
    public class AuthService(UserDbContext context, IConfiguration configuration, IEmailService emailService, RedisCacheService _cache,IMemoryCache _memoryCache) : IAuthService
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
            Console.WriteLine("credential has been changed ");
            user.hashedPassword = hasher.HashPassword(user, request.NewPassword);
            user.IsFirstLogin = false;
            context.Users.Update(user);
            await context.SaveChangesAsync();
            
            // Invalidate user cache after password change
            await _cache.RemoveMultipleAsync("AllUsers", $"User_{user.Id}");
            Console.WriteLine($"Cache invalidated after credential change for user: {username}");
            
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
            User user;
            try
            {
                // Use AsNoTracking for read-only operation to improve performance
                user = await context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Username == request.Username);
                    
                if (user is null)
                {
                    Console.WriteLine("User not found for username: " + request.Username);
                    return null;
                }

                // Re-attach for tracking since we need to update refresh token
                context.Entry(user).State = EntityState.Unchanged;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database error during login: {ex.Message}");
                throw new TimeoutException("Login request timed out. Please try again.", ex);
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
            string cacheKey =$"claims_{user.Id}";
            // Check if claims are already cached
            if (_memoryCache.TryGetValue(cacheKey, out List<Claim> cachedClaims))
            {
                Console.WriteLine($"Using cached claims for user {user.Username}");
                return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(claims: cachedClaims));
            }

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
            string cacheKey = $"User_{user.Id}";
            _memoryCache.Remove(cacheKey); // Remove user-specific cache entry
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
            
            // Use execution strategy to handle retries with transactions
            var strategy = context.Database.CreateExecutionStrategy();
            
            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();
                
                try
                {
                    var user = new User();
                    var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);
                    user.IsFirstLogin = true;
                    user.Username = request.Username;
                    user.hashedPassword = hashedPassword;
                    user.Email = request.Email;
                    user.Name = request.Name;
                    user.Role = request.Role;

                    context.Users.Add(user);
                    
                    // Save user first to get the generated ID
                    await context.SaveChangesAsync();
                    
                    // Create role-specific entity in the same transaction
                    if (request.Role.ToLower() == "admin")
                    {
                        var admin = new Admin()
                        {
                            Id = user.Id,
                            FirstName = user.Name
                        };
                        context.Admins.Add(admin);
                    }
                    else if (request.Role.ToLower() == "teacher")
                    {
                        var teacher = new Teacher()
                        {
                            Id = user.Id,
                            FirstName = user.Name,
                        };
                        context.Teachers.Add(teacher);
                    }
                    else if (request.Role.ToLower() == "student")
                    {
                        var student = new Student()
                        {
                            Id = user.Id,
                            FirstName = user.Name,
                        };
                        context.Students.Add(student);
                    }
                    
                    // Save role-specific entity
                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    
                    // Invalidate the cached user list using enhanced method
                    await _cache.TryRemoveAsync("AllUsers");
                    Console.WriteLine($"Cache invalidated after user registration: {user.Username}");

                    // Send email asynchronously without blocking the response
                    _ = Task.Run(async () => 
                    {
                        try
                        {
                            await emailService.SendEmailToUserAsync(user.Email, EmailTemplates.WelcomeSubject, 
                                EmailTemplates.WelcomeBody(user.Name, user.Username, request.Password));
                        }
                        catch (Exception ex)
                        {
                            // Log the email sending error but don't fail user creation
                            Console.WriteLine($"Failed to send welcome email to {user.Email}: {ex.Message}");
                        }
                    });

                    return new UserDto
                    {
                        Username = request.Username,
                        Name = request.Name,
                        Role = request.Role,
                        Email = request.Email
                    };
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

        public async Task<bool> RequsetPasswordReset( ResetPasswordRequestDto request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if( user is null)
            {
                return false;
            }
            var token = GenerateRefershToken();
            user.PasswordResetToken = token;
            user.PasswordResetTokenExpiryTime = DateTime.UtcNow.AddHours(1); // Token expires in 1 hour

            context.Users.Update(user);
            await context.SaveChangesAsync();
            
            // Send password reset email with token
            await emailService.SendEmailToUserAsync(user.Email, "Password Reset Request", 
                $"Your password reset token is: {token}. This token will expire in 1 hour.");
            
            return true;
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
            string  validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxy12345";
            Random random = new Random();
            // select one random character from  the string 
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }

            return new string(chars);




        }

        public async Task<bool> RequsetPasswordReset(string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if( user is null)
            {
                return false;

            }

            // generate a  reset token 
            var resetToken = GenerateRefershToken();
            user.PasswordResetToken = resetToken;
            user.PasswordResetTokenExpiryTime = DateTime.UtcNow.AddHours(1); // Set expiry time to 1 hour from now
            context.Users.Update(user);
            await context.SaveChangesAsync();
            // send the reset token to the  user email 
            var resetLink = $"http://localhost:5173/reset-password?email={user.Email}&token={HttpUtility.UrlEncode(resetToken)}";
            await emailService.SendEmailToUserAsync(user.Email, "Password Reset", $"Click the link to reset your password: {resetLink}");
            return true;

        }

        public async Task<bool> ResetPassword(ResetPasswordDto request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email &&
            u.PasswordResetToken == request.Token &&

            u.PasswordResetTokenExpiryTime > DateTime.UtcNow
            );

            if( user is null)
            {
                return false;
            }
            if  ( request.NewPassword != request.ConfirmPassword)
            {
                return false;
            }
                
            var hasher = new PasswordHasher<User>();
            user.hashedPassword = hasher.HashPassword(user, request.NewPassword);
            // invalidate token 

            user.PasswordResetToken = null;
            user.PasswordResetTokenExpiryTime = null;
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return true;

        }
    }
       
}
