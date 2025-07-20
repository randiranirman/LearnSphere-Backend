using CourseRegistration.Application.Dtos;
using CourseRegistration.Application.Repositories;
using CourseRegistration.Application.Services;
using CourseRegistration.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseRegistration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherRegistrationController : ControllerBase
    {
        private readonly ITeacherRegistrationService _teacherRegistrationService;
        private readonly ITeacherClassRegistrationRepository _teacherClassRegistrationRepository;
        private readonly ITeacherSubjectRepository _teacherSubjectRepository;

        public TeacherRegistrationController(
            ITeacherRegistrationService teacherRegistrationService,
            ITeacherClassRegistrationRepository teacherClassRegistrationRepository,
            ITeacherSubjectRepository teacherSubjectRepository)
        {
            _teacherRegistrationService = teacherRegistrationService;
            _teacherClassRegistrationRepository = teacherClassRegistrationRepository;
            _teacherSubjectRepository = teacherSubjectRepository;
        }

        /// <summary>
        /// Register a teacher for multiple classes and subjects
        /// </summary>
        /// <param name="request">Teacher registration request</param>
        /// <returns>Registration response</returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterTeacher([FromBody] TeacherRegistrationRequestDto request)
        {
            try
            {
                var result = await _teacherRegistrationService.RegisterTeacherAsync(request);
                
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Internal server error", Details = ex.Message });
            }
        }

        /// <summary>
        /// Get all pending teacher registrations (for admin)
        /// </summary>
        /// <returns>List of pending registrations</returns>
        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingRegistrations()
        {
            try
            {
                var pendingClassRegistrations = await _teacherClassRegistrationRepository.GetPendingRegistrationsAsync();
                var pendingSubjectRegistrations = await _teacherSubjectRepository.GetByStatusAsync(RegistrationStatus.Pending);

                var classRegistrationDtos = pendingClassRegistrations.Select(cr => new TeacherClassRegistrationDto
                {
                    TeacherRegistrationId = cr.TeacherRegistrationId,
                    TeacherId = cr.TeacherId,
                    ClassId = cr.ClassId,
                    ClassName = cr.Class?.Name ?? "",
                    SubjectId = cr.SubjectId,
                    SubjectName = cr.Subject?.Name ?? "",
                    EmployeeId = cr.EmployeeId,
                    Status = cr.Status,
                    RegisteredAt = cr.RegisteredAt,
                    ApprovedAt = cr.ApprovedAt,
                    ApprovedByAdminId = cr.ApprovedByAdminId,
                    Remarks = cr.Remarks
                });

                var subjectRegistrationDtos = pendingSubjectRegistrations.Select(sr => new TeacherSubjectDto
                {
                    Id = sr.Id,
                    TeacherId = sr.TeacherId,
                    SubjectId = sr.SubjectId,
                    SubjectName = sr.Subject?.Name ?? "",
                    EmployeeId = sr.EmployeeId,
                    Status = sr.Status,
                    RegisteredAt = sr.RegisteredAt,
                    ApprovedAt = sr.ApprovedAt,
                    ApprovedByAdminId = sr.ApprovedByAdminId,
                    Remarks = sr.Remarks,
                    IsActive = sr.IsActive
                });

                var result = new
                {
                    ClassRegistrations = classRegistrationDtos,
                    SubjectRegistrations = subjectRegistrationDtos
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Internal server error", Details = ex.Message });
            }
        }

        /// <summary>
        /// Get teacher registrations by teacher ID
        /// </summary>
        /// <param name="teacherId">Teacher ID</param>
        /// <returns>Teacher's registrations</returns>
        [HttpGet("teacher/{teacherId}")]
        public async Task<IActionResult> GetTeacherRegistrations(int teacherId)
        {
            try
            {
                var classRegistrations = await _teacherClassRegistrationRepository.GetByTeacherIdAsync(teacherId);
                var subjectRegistrations = await _teacherSubjectRepository.GetByTeacherIdAsync(teacherId);

                var result = new
                {
                    TeacherId = teacherId,
                    ClassRegistrations = classRegistrations,
                    SubjectRegistrations = subjectRegistrations
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Internal server error", Details = ex.Message });
            }
        }

        /// <summary>
        /// Get registrations by status
        /// </summary>
        /// <param name="status">Registration status</param>
        /// <returns>Registrations with specified status</returns>
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetRegistrationsByStatus(RegistrationStatus status)
        {
            try
            {
                var classRegistrations = await _teacherClassRegistrationRepository.GetByStatusAsync(status);
                var subjectRegistrations = await _teacherSubjectRepository.GetByStatusAsync(status);

                var result = new
                {
                    Status = status.ToString(),
                    ClassRegistrations = classRegistrations,
                    SubjectRegistrations = subjectRegistrations
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Internal server error", Details = ex.Message });
            }
        }

        /// <summary>
        /// Approve or reject a teacher registration (for admin)
        /// </summary>
        /// <param name="request">Approval request</param>
        /// <returns>Success status</returns>
        [HttpPost("approve")]
        public async Task<IActionResult> ApproveRegistration([FromBody] RegistrationApprovalRequestDto request)
        {
            try
            {
                var result = await _teacherRegistrationService.ApproveRegistration(request);
                
                if (result)
                {
                    return Ok(new { Message = "Registration status updated successfully" });
                }
                
                return BadRequest(new { Message = "Failed to update registration status" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Internal server error", Details = ex.Message });
            }
        }

        /// <summary>
        /// Get registration details by ID
        /// </summary>
        /// <param name="registrationId">Registration ID</param>
        /// <returns>Registration details</returns>
        [HttpGet("details/{registrationId}")]
        public async Task<IActionResult> GetRegistrationDetails(int registrationId)
        {
            try
            {
                // Try to get class registration first
                var classRegistration = await _teacherClassRegistrationRepository.GetByIdAsync(registrationId);
                if (classRegistration != null)
                {
                    return Ok(new { Type = "Class", Registration = classRegistration });
                }

                // Try to get subject registration
                var subjectRegistration = await _teacherSubjectRepository.GetByIdAsync(registrationId);
                if (subjectRegistration != null)
                {
                    return Ok(new { Type = "Subject", Registration = subjectRegistration });
                }

                return NotFound(new { Message = "Registration not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Internal server error", Details = ex.Message });
            }
        }

        /// <summary>
        /// Get registrations by class ID
        /// </summary>
        /// <param name="classId">Class ID</param>
        /// <returns>Registrations for specified class</returns>
        [HttpGet("class/{classId}")]
        public async Task<IActionResult> GetRegistrationsByClass(int classId)
        {
            try
            {
                var registrations = await _teacherClassRegistrationRepository.GetByClassIdAsync(classId);
                return Ok(registrations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Internal server error", Details = ex.Message });
            }
        }

        /// <summary>
        /// Get registrations by subject ID
        /// </summary>
        /// <param name="subjectId">Subject ID</param>
        /// <returns>Registrations for specified subject</returns>
        [HttpGet("subject/{subjectId}")]
        public async Task<IActionResult> GetRegistrationsBySubject(int subjectId)
        {
            try
            {
                var registrations = await _teacherSubjectRepository.GetBySubjectIdAsync(subjectId);
                return Ok(registrations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Internal server error", Details = ex.Message });
            }
        }
        [HttpPost("reject")]
        public async Task<IActionResult> RejectRegistration([FromBody] RegistrationApprovalRequestDto request)
        {
            request.Status = RegistrationStatus.Rejected;

            var result = await _teacherRegistrationService.ApproveRegistration(request);
            if (!result)
                return BadRequest("Failed to reject registration");

            return Ok("Registration rejected successfully");
        }

        /// <summary>
        /// Delete a teacher class registration (for admin only)
        /// </summary>
        /// <param name="adminId">Admin ID performing the deletion</param>
        /// <param name="registrationId">Class registration ID to delete</param>
        /// <returns>Success status</returns>
        [HttpDelete("delete/{adminId}/{registrationId}")]
        public async Task<IActionResult> DeleteRegistration(int adminId, int registrationId)
        {
            try
            {
                var result = await _teacherRegistrationService.DeleteRegistrationAsync(adminId, registrationId);
                
                if (result)
                {
                    return Ok(new { Message = "Registration deleted successfully" });
                }
                
                return NotFound(new { Message = "Registration not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Internal server error", Details = ex.Message });
            }
        }
    }
}
