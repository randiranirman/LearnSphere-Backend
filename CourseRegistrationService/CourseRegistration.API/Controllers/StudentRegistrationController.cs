using Microsoft.AspNetCore.Mvc;
using CourseRegistration.Application.Interfaces;
using CourseRegistration.Application.Dtos;

namespace CourseRegistration.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentRegistrationController : ControllerBase
    {
        private readonly IStudentRegistrationService _registrationService;

        public StudentRegistrationController(IStudentRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        /// <summary>
        /// Register a student for a class with multiple subjects
        /// </summary>
        /// <param name="request">Registration request</param>
        /// <returns>Registration response</returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterStudent(StudentRegistrationRequestDto request)
        {
            try
            {
                Console.WriteLine(request);
                var result = await _registrationService.RegisterStudentAsync(request);
                Console.WriteLine(result);
                
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Registration failed", Details = ex.Message });
            }
        }

        /// <summary>
        /// Get all registrations for a specific student
        /// </summary>
        /// <param name="studentId">Student ID</param>
        /// <returns>List of student registrations</returns>
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetStudentRegistrations(int studentId)
        {
            try
            {
                var registrations = await _registrationService.GetStudentRegistrationsAsync(studentId);
                return Ok(registrations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Failed to retrieve registrations", Details = ex.Message });
            }
        }

        /// <summary>
        /// Get all subjects that a specific student is enrolled in
        /// </summary>
        /// <param name="studentId">Student ID</param>
        /// <returns>List of subjects the student is enrolled in</returns>
        [HttpGet("student/{studentId}/subjects")]
        public async Task<IActionResult> GetStudentSubjects(int studentId)
        {
            try
            {
                var subjects = await _registrationService.GetStudentSubjectsAsync(studentId);
                return Ok(subjects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Failed to retrieve student subjects", Details = ex.Message });
            }
        }

        /// <summary>
        /// Get all pending registrations (for admins)
        /// </summary>
        /// <returns>List of pending registrations</returns>
        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingRegistrations()
        {
            try
            {
                var registrations = await _registrationService.GetPendingRegistrationsAsync();
                return Ok(registrations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Failed to retrieve pending registrations", Details = ex.Message });
            }
        }

        /// <summary>
        /// Get a specific registration by ID
        /// </summary>
        /// <param name="registrationId">Registration ID</param>
        /// <returns>Registration details</returns>
        [HttpGet("{registrationId}")]
        public async Task<IActionResult> GetRegistrationById(int registrationId)
        {
            try
            {
                var registration = await _registrationService.GetRegistrationByIdAsync(registrationId);
                
                if (registration == null)
                {
                    return NotFound($"Registration with ID {registrationId} not found");
                }
                
                return Ok(registration);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Failed to retrieve registration", Details = ex.Message });
            }
        }

        /// <summary>
        /// Approve a student registration (admin only)
        /// </summary>
        /// <param name="registrationId">Registration ID</param>
        /// <param name="adminId">Admin ID</param>
        /// <returns>Success status</returns>
        [HttpPost("{registrationId}/approve")]
        public async Task<IActionResult> ApproveRegistration(int registrationId, [FromQuery] int adminId)
        {
            try
            {
                var result = await _registrationService.ApproveRegistrationAsync(registrationId, adminId);
                
                if (result)
                {
                    return Ok(new { Message = "Registration approved successfully" });
                }
                
                return NotFound("Registration not found or could not be approved");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Failed to approve registration", Details = ex.Message });
            }
        }

        /// <summary>
        /// Reject a student registration (admin only)
        /// </summary>
        /// <param name="registrationId">Registration ID</param>
        /// <param name="adminId">Admin ID</param>
        /// <param name="reason">Rejection reason</param>
        /// <returns>Success status</returns>
        [HttpPost("{registrationId}/reject")]
        public async Task<IActionResult> RejectRegistration(int registrationId, [FromQuery] int adminId, [FromBody] string reason)
        {
            try
            {
                var result = await _registrationService.RejectRegistrationAsync(registrationId, adminId, reason);
                
                if (result)
                {
                    return Ok(new { Message = "Registration rejected successfully" });
                }
                
                return NotFound("Registration not found or could not be rejected");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Failed to reject registration", Details = ex.Message });
            }
        }
    }
}
