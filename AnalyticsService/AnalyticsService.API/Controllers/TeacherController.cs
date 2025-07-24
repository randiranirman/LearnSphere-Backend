using AnalyticsService.Application.Querries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnalyticsService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController(ISender sender) : ControllerBase
    {
        // load all classes(subjects / grade) -> given teacherId
        [HttpGet]
        //[Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetSubjectsByTeacherId([FromQuery] int teacherId) // if we use Authorized version we don't need teacherId to pass as a input parameter
        {
            //var teacherIdClaim = User.FindFirst("teacherId")?.Value;
            //if (string.IsNullOrEmpty(teacherIdClaim) || !int.TryParse(teacherIdClaim, out var teacherId))
            //    return Unauthorized("Invalid or missing teacher ID");
            var result = await sender.Send(new GetAllSubjectsByTeacherIdQuery(teacherId));
            return Ok(result);
        }
    }
}
