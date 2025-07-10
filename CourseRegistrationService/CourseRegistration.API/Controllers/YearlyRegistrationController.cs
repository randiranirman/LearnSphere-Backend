using Microsoft.AspNetCore.Mvc;
using CourseRegistration.Application.Interfaces;
using CourseRegistration.Application.Dtos;
using CourseRegistration.Domain.Models;

namespace CourseRegistration.API.Controllers
{
    [ApiController]
    [Route("registrations/[controller]")]
    public class YearlyRegistrationController : ControllerBase
    {
        private readonly IYearlyRegistrationService _registrationService;

        public YearlyRegistrationController(IYearlyRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        // Student Registration Endpoints
        [HttpPost("student")]
        public async Task<IActionResult> RegisterStudent([FromBody] StudentYearlyRegistrationRequest request)
        {
            try
            {
                var registrationId = await _registrationService.RegisterStudentAsync(request);
                return Ok(new { RegistrationId = registrationId, Message = "Student registration submitted successfully. Awaiting admin approval." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("student/{studentId}/registrations")]
        public async Task<IActionResult> GetStudentRegistrations(int studentId)
        {
            var registrations = await _registrationService.GetStudentRegistrationsAsync(studentId);
            return Ok(registrations);
        }

        // Teacher Registration Endpoints
        [HttpPost("teacher")]
        public async Task<IActionResult> RegisterTeacher([FromBody] TeacherYearlyRegistrationRequest request)
        {
            try
            {
                var registrationId = await _registrationService.RegisterTeacherAsync(request);
                return Ok(new { RegistrationId = registrationId, Message = "Teacher registration submitted successfully. Awaiting admin approval." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("teacher/{teacherId}/registrations")]
        public async Task<IActionResult> GetTeacherRegistrations(int teacherId)
        {
            var registrations = await _registrationService.GetTeacherRegistrationsAsync(teacherId);
            return Ok(registrations);
        }

        // Admin Approval Endpoints
        [HttpPost("admin/approve/student")]
        public async Task<IActionResult> ApproveStudentRegistration([FromBody] RegistrationApprovalRequest request)
        {
            var result = await _registrationService.ApproveStudentRegistrationAsync(request);
            if (result)
            {
                return Ok(new { Message = "Student registration approved successfully." });
            }
            return BadRequest(new { Error = "Failed to approve student registration." });
        }

        [HttpPost("admin/approve/teacher")]
        public async Task<IActionResult> ApproveTeacherRegistration([FromBody] RegistrationApprovalRequest request)
        {
            var result = await _registrationService.ApproveTeacherRegistrationAsync(request);
            if (result)
            {
                return Ok(new { Message = "Teacher registration approved successfully." });
            }
            return BadRequest(new { Error = "Failed to approve teacher registration." });
        }

        [HttpGet("admin/pending/students")]
        public async Task<IActionResult> GetPendingStudentRegistrations()
        {
            var registrations = await _registrationService.GetPendingStudentRegistrationsAsync();
            return Ok(registrations);
        }

        [HttpGet("admin/pending/teachers")]
        public async Task<IActionResult> GetPendingTeacherRegistrations()
        {
            var registrations = await _registrationService.GetPendingTeacherRegistrationsAsync();
            return Ok(registrations);
        }

        // Validation Endpoints
        [HttpPost("validate/student")]
        public async Task<IActionResult> ValidateStudentRegistration([FromBody] StudentYearlyRegistrationRequest request)
        {
            var isValid = await _registrationService.ValidateStudentRegistrationAsync(request);
            return Ok(new { IsValid = isValid });
        }

        [HttpPost("validate/teacher")]
        public async Task<IActionResult> ValidateTeacherRegistration([FromBody] TeacherYearlyRegistrationRequest request)
        {
            var isValid = await _registrationService.ValidateTeacherRegistrationAsync(request);
            return Ok(new { IsValid = isValid });
        }

        // Reports
        [HttpGet("reports/students/{status}")]
        public async Task<IActionResult> GetStudentRegistrationsByStatus(RegistrationStatus status)
        {
            var registrations = await _registrationService.GetStudentRegistrationsByStatusAsync(status);
            return Ok(registrations);
        }

        [HttpGet("reports/teachers/{status}")]
        public async Task<IActionResult> GetTeacherRegistrationsByStatus(RegistrationStatus status)
        {
            var registrations = await _registrationService.GetTeacherRegistrationsByStatusAsync(status);
            return Ok(registrations);
        }
    }
}

