using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewAnalyticsService.Application.DTOs;
using NewAnalyticsService.Application.Interfaces;

namespace NewAnalyticsService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentViewController(IStudentDetailsHttpRepository studentDetailsHttpRepository, IStudentsRepository studentsRepository) : ControllerBase
    {
        [HttpGet("get-student-details-by-subjectID")]
        public async Task<IActionResult> GetAllStudentsDetailsBySubjectId([FromQuery] int subjectId)
        {
            var result = await studentDetailsHttpRepository.GetAllStudentsDetailsBySubjectId(subjectId);
            if (result is null)
            {
                return BadRequest();
            }
            var response = result.Select(student => new GetAllStudentsBySubjectIdResponseDTO
            {
                Id = student.StudentID,
                FullName = student.StudentName,
                IndexNumber = student.IndexNumber
            }).ToList();
            return Ok(response);
        }

        [HttpGet("get-all-student-marks-by-assignmentID")]
        public async Task<IActionResult> GetAllStudentMarksByAssignmentId([FromQuery] int assignmentId)
        {
            var result = await studentsRepository.GetAllStudentMarksByAssignmentId(assignmentId);
            if (result is null) return BadRequest();
            return Ok(result);
        }
    }
}
