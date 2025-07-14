using CourseRegistration.Application.Dtos;

namespace CourseRegistration.Application.Interfaces
{
    public interface IStudentHttpService
    {
        Task<StudentDto?> GetStudentByIdAsync(int studentId);
        Task<bool> ValidateStudentExistsAsync(int studentId);
        Task<List<StudentDto>> GetStudentsByIdsAsync(List<int> studentIds);
    }
}
