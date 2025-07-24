using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Dtos;
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
                return NotFound(new { message = $"User with username {username} not found" });
            }

            return Ok(new { message = $"User with username {username} deleted successfully" });
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
        [HttpGet("students/{id}")]
        public async Task<ActionResult<StudentDto>> GetStudentByID(int id)
        {
            var result = await _userService.GetStudentByID(id);
            if (result == null)
            {
                return NotFound("student is not found ");

            }

            return result;
        }

        [HttpGet("students/get-all-students")]
        public async Task<IActionResult> GetAllStudentsRegistered()
        {
            var result = await _userService.GetAllStudentsRegistered();
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("students/get-student-by-indexNo")]
        public async Task<IActionResult> GetStudentByIndexNo([FromQuery] string indexNo)
        {
            var result = await _userService.GetStudentByIndexNoAsync(indexNo);
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("teachers/{id}")]
        public async Task<ActionResult<TeacherDto>> GetTeacherByID(int id)
        {
            var result = await _userService.GetTeacherByID(id);
            if (result == null)
            {
                return NotFound("teacher is not found ");
            }
            return result;




        }
        [HttpPut("edit-teacher/{id}")]
        public async Task<ActionResult<TeacherDto>> EditTeacherDetailsById(int id, TeacherDto request)
        {
            var result = await _userService.EditTeacherDetailsById(id, request);
            if (result == null)
            {
                return NotFound("teacher is not found ");
            }
            return result;

        }

        [HttpGet("teachers/get-all-teachers")]
        public async Task<IActionResult> GetAllTeachersThatRegistered()
        {
            var result = await _userService.GetAllTeachersRegistered();
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpPut("edit-student/{id}")]
        public async Task<ActionResult<StudentDto>> EditStudentDetailsByID(int id, StudentDto request)
        {
            var result = await _userService.EditStudentDetailsByID(id, request);
            if (result == null)
            {
                return NotFound("student is not found ");
            }
            return result;
        }


        [HttpPost("getstudentsbyIdList")]

        public async Task<IActionResult> GetStudentDetailsByIdList([FromBody] StudentListDto request)
        {


            var result = await _userService.getDeatilsStudentsByIds(request.StudentList);
            if(result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet("get-students-and-teachers-count")]
        public async Task<IActionResult> GetNoOfStudentsAndNoOfTeachers()
        {
            var result = await _userService.GetNoOfTeachersAndNoOfStudents();
            if (result is null) return BadRequest();
            return Ok(result);
        }

    }



}
