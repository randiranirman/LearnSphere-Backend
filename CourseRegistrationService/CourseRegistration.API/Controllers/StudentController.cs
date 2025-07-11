using Microsoft.AspNetCore.Mvc;
using CourseRegistration.Application.Interfaces;
using CourseRegistration.Application.Dtos;

namespace CourseRegistration.API.Controllers
{
    [ApiController]
    [Route("registrations/students/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentHttpService _studentHttpService;

        public StudentController(IStudentHttpService studentHttpService)
        {
            _studentHttpService = studentHttpService;
        }

        /// <summary>
        /// Get student information by ID from User Management Service
        /// </summary>
        /// <param name="studentId">Student ID</param>
        /// <returns>Student information</returns>
        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetStudentById(int studentId)
        {
            try
            {
                var student = await _studentHttpService.GetStudentByIdAsync(studentId);
                
                if (student == null)
                {
                    return NotFound($"Student with ID {studentId} not found");
                }
                
                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Internal server error", Details = ex.Message });
            }
        }

        /// <summary>
        /// Validate if a student exists in the User Management Service
        /// </summary>
        /// <param name="studentId">Student ID</param>
        /// <returns>Boolean indicating if student exists</returns>
        [HttpGet("{studentId}/exists")]
        public async Task<IActionResult> ValidateStudentExists(int studentId)
        {
            try
            {
                var exists = await _studentHttpService.ValidateStudentExistsAsync(studentId);
                return Ok(new { StudentId = studentId, Exists = exists });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Internal server error", Details = ex.Message });
            }
        }

        /// <summary>
        /// Get multiple students by their IDs
        /// </summary>
        /// <param name="studentIds">List of student IDs</param>
        /// <returns>List of students</returns>
        [HttpPost("batch")]
        public async Task<IActionResult> GetStudentsByIds([FromBody] List<int> studentIds)
        {
            try
            {
                if (studentIds == null || !studentIds.Any())
                {
                    return BadRequest("Student IDs list cannot be empty");
                }

                var students = await _studentHttpService.GetStudentsByIdsAsync(studentIds);
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Internal server error", Details = ex.Message });
            }
        }

        /// <summary>
        /// Register a student for course registration using their existing ID
        /// </summary>
        /// <param name="studentId">Student ID from User Management Service</param>
        /// <param name="registrationRequest">Registration details</param>
        /// <returns>Registration result</returns>
        [HttpPost("{studentId}/register")]
        public async Task<IActionResult> RegisterStudentForCourse(int studentId, [FromBody] StudentCourseRegistrationRequest registrationRequest)
        {
            try
            {
                // First, validate that the student exists
                var studentExists = await _studentHttpService.ValidateStudentExistsAsync(studentId);
                if (!studentExists)
                {
                    return NotFound($"Student with ID {studentId} not found in User Management Service");
                }

                // Get student details
                var student = await _studentHttpService.GetStudentByIdAsync(studentId);
                if (student == null)
                {
                    return NotFound($"Unable to retrieve student details for ID {studentId}");
                }

                // Create registration request with student details
                var yearlyRegistrationRequest = new StudentYearlyRegistrationRequest
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    IndexNumber = student.IndexNumber,
                    Email = student.Email,
                    ContactNumber = student.ContactNumber,
                    Address = student.Address,
                    DateOfBirth = student.DateOfBirth,
                    ParentContactNumber = student.ParentContactNumber,
                    ParentName = student.ParentName,
                    Grade = student.Grade,
                    ClassId = registrationRequest.ClassId,
                    SubjectIds = registrationRequest.SubjectIds,
                    ExistingStudentId = studentId
                };

                // Here you would typically call your registration service
                // For now, we'll just return a success response
                return Ok(new
                {
                    Message = "Student registration prepared successfully",
                    StudentId = studentId,
                    StudentName = student.StudentName,
                    ClassId = registrationRequest.ClassId,
                    SubjectIds = registrationRequest.SubjectIds
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Internal server error", Details = ex.Message });
            }
        }
    }
}
