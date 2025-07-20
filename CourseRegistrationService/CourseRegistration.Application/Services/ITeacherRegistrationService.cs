using CourseRegistration.Application.Dtos;
using CourseRegistration.Domain.Models;

namespace CourseRegistration.Application.Services
{
    public interface ITeacherRegistrationService
    {
        Task<TeacherRegistrationResponseDto> RegisterTeacherAsync(TeacherRegistrationRequestDto request);
        Task<bool> ApproveRegistration(RegistrationApprovalRequestDto request);
        Task<bool> DeleteRegistrationAsync(int adminId, int registrationId);
    }
}
