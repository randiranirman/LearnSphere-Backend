using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NewAnalyticsServcie.Application.Interfaces;

namespace NewAnalyticsService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController(ITeacherSubjectHttpService teacherSubjectHttpService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllSubjectsByTeacherId([FromQuery] int teacherId)
        {
            var result = await teacherSubjectHttpService.GetSubjectsByTeacherIdAsync(teacherId);
            return Ok(result);
        }
    }
}
