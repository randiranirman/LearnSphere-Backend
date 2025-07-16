using AnalyticsService.Application.DTOs;
using AnalyticsService.Application.Querries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnalyticsService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController(ISender sender) : ControllerBase
    {
        [HttpGet]
        // this is for retrive no of students and no of teachers in the system
        public async Task<IActionResult> GetNoOfStudentsAndNoOfTeacher()
        {
            var response = await sender.Send(new GetNoOfStudentsAndNoOfTeacherQuery());
            if (response is null)
            {
                return NotFound("Not data found!");
            }
            return Ok(response);
        }

        [HttpGet("/teachers")]
        public async Task<IActionResult> GetAllTheRegisteredTeachersWithSubjectCount()
        {
            var response = await sender.Send(new GetTeachersWithSubjectsCountsQuery());
            if (response is null)
            {
                return NotFound("No teacher has been registered!");
            }
            return Ok(response);
        }

        [HttpGet("/teacher")]
        public async Task<IActionResult> GetTeacherSubejctsWithStudentCountAsync([FromQuery] int teacherId)
        {
            var resposne = await sender.Send(new GetTeacherSubejctsWithStudentCountQuery(teacherId));
            if (resposne is null)
            {
                return NotFound("No subject is assigned to this teacher!");
            }
            return Ok(resposne);
        } 
    }
}
