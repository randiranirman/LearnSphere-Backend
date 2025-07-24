using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewAnalyticsService.Application.DTOs;
using NewAnalyticsService.Application.Interfaces;

namespace NewAnalyticsService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarksController(IMarksRepository marksRepository) : ControllerBase
    {

        [HttpGet("get-marks")]
        public async Task<IActionResult> GetAllSubmissionsMarksByAssignmentId([FromQuery] int assignmentId)
        {
            var result = await marksRepository.GetAllSubmissionsMarksByAssignmentId(assignmentId);
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpPost("submit-marks/{assignmentId:int}")]
        public async Task<IActionResult> SubmitMarks([FromRoute] int assignmentId, [FromBody] SubmitMarksRequestDTO submitMarksRequest)
        {
            var result = await marksRepository.SubmitMarks(assignmentId, submitMarksRequest);
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpPut("edit-marks")]
        public async Task<IActionResult> EditMarks([FromQuery] int submissionId, [FromQuery] int newMarks)
        {
            var result = await marksRepository.EditMarks(submissionId, newMarks);
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("isAllocateMarks")]
        public async Task<IActionResult> GetIsMarksAllocatedStatus([FromQuery] int assignmentId)
        {
            var result = await marksRepository.GetIsMarkAllocatedStatusByAssignmentId(assignmentId);
            if (result is null) return NotFound(null);
            return Ok(result);
        }

        [HttpPost("allocate-marks")]
        public async Task<IActionResult> AllocateMarksForAssignment([FromQuery] int assignmentId)
        {
            var result = await marksRepository.CreateMarkAllocation(assignmentId);
            return Ok(result);
        }
    }
}
