using CourseRegistration.Application.Interfaces;
using CourseRegistration.Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseRegistration.API.Controllers
{
    [Route("registrations/teachers/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {

        private readonly ITeacherHttpService teacherHttpService;
        private readonly ITeacherSubjectRepository teacherSubjectRepository;
        public TeacherController(ITeacherHttpService teacherHttpService, ITeacherSubjectRepository teacherSubjectRepository)
        {
            this.teacherHttpService = teacherHttpService;
            this.teacherSubjectRepository = teacherSubjectRepository;
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

        [HttpGet("get-all-teachers-with-subjectCount")]
        public async Task<IActionResult> GetAllTeachersWithSubjectCount()
        {
            var result = await teacherSubjectRepository.GetAllTeachersWithSubjectCount();
            if (result is null) return BadRequest();

            return Ok(result);
        }

    }
}
