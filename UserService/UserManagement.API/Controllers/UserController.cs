using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Repositories;
using UserManagement.Domain.Domain;

namespace UserManagement.API.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            if (users is null || !users.Any())
            {

                return NotFound("No users found");
            }

            return Ok(users);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user is null)
            {
                return NotFound($"User with ID {id} not found");
            }
            return Ok(user);

        }

        [HttpDelete("{username}")]
        public async Task<ActionResult> DeleteUser(string username)
        {

            var result = await _userService.DeleteUserAsync(username);
            if (!result)
            {
                return NotFound("User with username " + username + " Not Found");
            }

            return Ok("User with username " + username + " deleted successfully");






        }


        [HttpDelete("delete/{id}")]

        public async Task<ActionResult> DeletUserByIdController(int id)
        {
            var result = await _userService.DeleteUserByIdAsync(id);
            if (!result)
            {
                return NotFound("User not found ");
            }

            return Ok(result);
        }


    }


}
