using CourseRegistration.Application.Dtos;
using CourseRegistration.Domain.Models;

namespace CourseRegistration.Application.Interfaces
{
    public interface IStudentRegistrationService
    {
        Task<StudentRegistrationResponseDto> RegisterStudentAsync(StudentRegistrationRequestDto request);
        Task<bool> ApproveRegistrationAsync(int registrationId, int adminId);
        Task<bool> RejectRegistrationAsync(int registrationId, int adminId, string reason);
        Task<IEnumerable<StudentRegistrationDto>> GetStudentRegistrationsAsync(int studentId);
        Task<IEnumerable<StudentRegistrationDto>> GetPendingRegistrationsAsync();
        Task<IEnumerable<StudentRegistrationDto>> GetApprovedRegistrationsAsync();
        Task<StudentRegistrationDto?> GetRegistrationByIdAsync(int registrationId);
        Task<IEnumerable<SubjectDto>> GetStudentSubjectsAsync(int studentId);
    }
}
