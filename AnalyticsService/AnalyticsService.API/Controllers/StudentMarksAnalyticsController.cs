using AnalyticsService.Application.Querries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnalyticsService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentMarksAnalyticsController(ISender sender) : ControllerBase
    {
        // get all the students for a subject
        [HttpGet("{subjectId:int}")]
        public async Task<IActionResult> GetAllStudents([FromRoute] int subjectId)
        {
            // want to impliment
            var result = await sender.Send(new GetAllStudentsBySubjectIdQuerry(subjectId));
            if (result is null || !result.Any())
            {
                return NotFound($"No students found for subject ID {subjectId}.");
            }
            return Ok(result);
        }

        // get all the marks for each assignment for a particular student
        [HttpGet]
        public async Task<IActionResult> GetAllMarksByStudentId([FromQuery] int studentId)
        {
            var result = await sender.Send(new GetAllMarksByStudentIdQuerry(studentId));
            return Ok(result);
        }
    }
}
