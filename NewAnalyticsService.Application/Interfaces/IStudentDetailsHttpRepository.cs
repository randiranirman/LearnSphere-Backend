using NewAnalyticsService.Application.DTOs;

namespace NewAnalyticsService.Application.Interfaces
{
    public interface IStudentDetailsHttpRepository
    {
        public Task<IEnumerable<StudentDetailsDTO>> GetAllStudentsDetailsBySubjectId(int subjectId);
    }
}
