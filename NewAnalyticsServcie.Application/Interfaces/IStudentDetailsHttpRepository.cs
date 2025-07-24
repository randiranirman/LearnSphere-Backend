using NewAnalyticsServcie.Application.DTOs;

namespace NewAnalyticsServcie.Application.Interfaces
{
    public interface IStudentDetailsHttpRepository
    {
        public Task<IEnumerable<StudentDetailsDTO>> GetAllStudentsDetailsBySubjectId(int subjectId);
    }
}
