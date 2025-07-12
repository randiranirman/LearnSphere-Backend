using AnalyticsService.Application.Querries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnalyticsService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController(ISender sender) : ControllerBase
    {
        // load all the assignments for a given subject
        [HttpGet("{subjectId:int}")]
        public async Task<IActionResult> GetAllAssignmentsBySubjectId([FromRoute] int subjectId)
        {
            var result = await sender.Send(new GetAllAssignmentsBySubjectQuerry(subjectId));
            return Ok(result);
        }

        // assignmentId -> all the students that submit answers for a given assignment
        [HttpGet]
        public async Task<IActionResult> GetAllStudentsByAssignmentId([FromQuery] int assignmentId)
        {
            var result = await sender.Send(new GetAllStudentsByAssignmentIdQuery(assignmentId));
            return Ok(result);
        }
    }
}
