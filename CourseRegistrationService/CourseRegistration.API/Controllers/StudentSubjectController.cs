using Microsoft.AspNetCore.Mvc;
using CourseRegistration.Application.Interfaces;
using CourseRegistration.Application.Dtos;

namespace CourseRegistration.API.Controllers
{
    [ApiController]
    [Route("studentSubjects/[controller]")]
    public class StudentSubjectController : ControllerBase
    {
        private readonly IStudentRegistrationService _registrationService;

        public StudentSubjectController(IStudentRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        /// <summary>
        /// Get all subjects that a specific student is enrolled in
        /// </summary>
        /// <param name="studentId">Student ID</param>
        /// <returns>List of subjects the student is enrolled in</returns>
        [HttpGet("subject/{studentId}")]
        public async Task<IActionResult> GetStudentSubjects(int studentId)
        {
            try
            {
                if (studentId <= 0)
                {
                    return BadRequest("Invalid student ID");
                }

                var subjects = await _registrationService.GetStudentSubjectsAsync(studentId);
                
                if (subjects == null || !subjects.Any())
                {
                    return Ok(new List<SubjectDto>());
                }

                return Ok(subjects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    Error = "Failed to retrieve student subjects", 
                    Details = ex.Message 
                });
            }
        }

        /// <summary>
        /// Check if a student is enrolled in a specific subject
        /// </summary>
        /// <param name="studentId">Student ID</param>
        /// <param name="subjectId">Subject ID</param>
        /// <returns>Boolean indicating enrollment status</returns>
        [HttpGet("student/{studentId}/subject/{subjectId}")]
        public async Task<IActionResult> IsStudentEnrolledInSubject(int studentId, int subjectId)
        {
            try
            {
                if (studentId <= 0 || subjectId <= 0)
                {
                    return BadRequest("Invalid student ID or subject ID");
                }

                var subjects = await _registrationService.GetStudentSubjectsAsync(studentId);
                var isEnrolled = subjects.Any(s => s.SubjectId == subjectId);

                return Ok(new { IsEnrolled = isEnrolled });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    Error = "Failed to check enrollment status", 
                    Details = ex.Message 
                });
            }
        }
    }
}
