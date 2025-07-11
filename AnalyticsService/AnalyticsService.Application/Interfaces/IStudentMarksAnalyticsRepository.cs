using AnalyticsService.Application.DTOs;

namespace AnalyticsService.Application.Interfaces
{
    public interface IStudentMarksAnalyticsRepository
    {
        public Task<IEnumerable<StudentDTO>> GetAllStudentsAsync(int subjectId);
        public Task<IEnumerable<AllMarksByStudentIdDTO>> GetAllMarksByStudentIdAsync(int studentId);
    }
}
