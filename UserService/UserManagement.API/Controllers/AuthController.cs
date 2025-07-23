using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Dtos;
using UserManagement.Application.Repositories;
using UserManagement.Domain.Domain;
using UserManagement.Domain.Repositories;

namespace UserManagement.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService, IEmailService emailService) : ControllerBase
    {
        public static User user = new User();


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(UserDto request)
        {
            var user = await authService.RegisterAsync(request);
            if (user is null)
            {
                return BadRequest(new { message = "Username already exits" });
            }


            return Ok(
                new
                {
                    user.Username,
                    user.Email,
                    user.Name,
                    user.Role
                }
                );

        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
        {
            var result = await authService.LoginAsync(request);
            if (result is null)
            {
                return BadRequest("Invalid username or password");


            }
            return Ok(result);
        }


        [Authorize(Roles = "admin")]
        [HttpGet("admin")]
        public IActionResult AuthnticatedOnly()
        {

            return Ok("You are authenticated and authorized as admin");
        }

        [HttpPost("refreshToken")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await authService.RefreshTokenAsync(request);
            if (result is null || result.AccessToken is null || result.RefreshToken is null)
            {
                return Unauthorized("Invalid refresh token");


            }


            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Invalid username");
            }
            var result = await authService.LogoutAsync(username);
            if (result)
            {
                return Ok("Logged out successfully");
            }

            return Ok("Failed to logout");




        }
        [HttpPost("update-credentials/{username}")]
        public async Task<IActionResult> UpdateCredentials(string username, ChangeCredentialsDto request)
        {

            if (username is null)
            {
                return Unauthorized();
            }

            var result = await authService.ChangeCredentials(username, request);
            if (!result)
                return BadRequest("Invalid credentials or password mismatch.");

            return Ok("Credentials updated successfully. Please log in again.");

        }


        [HttpPost("request-password-reset")]

        public async Task<IActionResult> RequestPasswordReset([FromBody] PasswordResetRequsetDto requsetDto)
        {
            var result = await authService.RequsetPasswordReset( requsetDto.Email);
            if (!result)
            {
                return BadRequest("Failed to request password reset. Please check your email.");
            }

            return Ok(new { message = "Password reset link has been sent to your email." });


        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto request)
        {
            if (request is null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.NewPassword) || string.IsNullOrEmpty(request.Token))
            {
                return BadRequest("Invalid request data.");
            }
            var result = await authService.ResetPassword(request);
            if (!result)
            {
                return BadRequest("Failed to reset password. Please check your token and email.");
            }
            return Ok(new { message = "Password has been reset successfully." });
        }

    }
}
