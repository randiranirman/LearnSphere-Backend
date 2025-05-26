using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Dtos;

namespace UserManagement.Domain.Repositories
{
    public interface IAuthService
    {

        Task<UserDto?> RegisterAsync(UserDto request);
        Task<TokenResponseDto?> LoginAsync(UserDto request);
        Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request);
        Task<bool> LogoutAsync(string username);
        Task<bool> IsFirstLogin(UserDto request);
        Task<bool> UpdateFirstLoginStatus(UserDto request);
            
        Task<bool> ChangeCredentials(String username, ChangeCredentialsDto request);
        Task<bool> RequsetPasswordReset(String email);
        Task<bool> ResetPassword(ResetPasswordDto request); 

    }
}
