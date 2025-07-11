using CourseRegistration.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseRegistration.API.Controllers
{
    [Route("registrations/teachers/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {

        private readonly ITeacherHttpService teacherHttpService;
        public TeacherController(ITeacherHttpService teacherHttpService)
        {
            this.teacherHttpService = teacherHttpService;
        }

        [HttpGet("{teacherId}")]
        public async Task<IActionResult> GetTeacherById(int teacherId)
        {
            try
            {
                var teacher = await teacherHttpService.GetTeacherByIdAsync(teacherId);
                if (teacher == null)
                {
                    return NotFound($"Teacher with ID {teacherId} not found");
                }
                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "Internal server error", Details = ex.Message });
            }
        }
    }
}
