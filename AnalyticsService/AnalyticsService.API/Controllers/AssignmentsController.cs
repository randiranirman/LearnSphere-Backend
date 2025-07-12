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
    }
}
