using Microsoft.AspNetCore.Mvc;
using NewAnalyticsService.Application.Interfaces;

namespace NewAnalyticsService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticController(IAnalyticsRepository analyticsRepository) : ControllerBase
    {
        [HttpGet("get-all-subjects-with-assignment-and-student-counts-by-teacherId")]
        public async Task<IActionResult> GetAllSubjectsWithAssignmentsAndStudentCountByTeacherId([FromQuery] int teacherId)
        {
            var result = await analyticsRepository.GetAllSubjectsWithAssignmentsAndSubmissionsByTeacherId(teacherId);
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("get-all-marks-by-subjectId-and-studentId")]
        public async Task<IActionResult> GetAllSubmissionMarksBySubjectIdAndStudentId([FromQuery] int subjectId, [FromQuery] int studentId)
        {
            var result = await analyticsRepository.GetAllAssignmentMarksByStudentIdAndSubjectId(studentId, subjectId);
            if (result is null) return BadRequest();
            return Ok(result);
        }
    }
}
