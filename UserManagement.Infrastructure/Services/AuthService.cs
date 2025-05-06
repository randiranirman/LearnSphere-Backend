using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserManagement.Application.Dtos;
using UserManagement.Domain.Domain;
using UserManagement.Domain.Repositories;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Services
{
    public class AuthService(UserDbContext context, IConfiguration configuration) : IAuthService
    {
        public Task<bool> ChangeCredentials(string username, ChangeCredentialsDto request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsFirstLogin(UserDto request)
        {
            throw new NotImplementedException();
        }

        // user log in functionality 

        public async Task<TokenResponseDto?> LoginAsync(UserDto request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user is null)
            {
                return null;
            }

            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.hashedPassword, request.Password) == PasswordVerificationResult.Success)
            {
                // returning the access token and the refresh token to the response  
                return new TokenResponseDto
                {
                    AccessToken = createTokenResponse(user),
                    RefreshToken = await GenereateAndSaveRefreshToken(user)
                };
            }

            return null;
        }

        private string createTokenResponse(User user)
        {
            throw new NotImplementedException();
        }

        private async Task<string> GenereateAndSaveRefreshToken(User user)
        {
            throw new NotImplementedException();
        }

        private string createToken(User user)
        {
            var claims = new List<Claim>
            {

                new Claim( ClaimTypes.Name, user.Username),
                new Claim( ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(" isFirstLogin", user.IsFirstLogin.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<String>("AppSettings:Token")!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new JwtSecurityToken(
                
                )

           
        }

        public Task<bool> LogoutAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto?> RegisterAsync(UserDto request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateFirstLoginStatus(UserDto request)
        {
            throw new NotImplementedException();
        }
    }
}
