using FileStorage.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController(IAssignmentRepository assignmentRepository) : ControllerBase
    {
        [HttpGet("get-all-assignments-by-subjectID")]
        public async Task<IActionResult> GetAllAssignmentsBySubjectId([FromQuery] int subjectId)
        {
            var result = await assignmentRepository.GetAllAssignmentDetailsFromSubjectId(subjectId);
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("get-assignment-count-by-subjectId")]
        public async Task<IActionResult> GetAssignmentCountBySubjectId([FromQuery] int subjectId)
        {
            var result = await assignmentRepository.GetAssignmentCountBySubjectIdAsync(subjectId);
            return Ok(result);
        }
    }
}
