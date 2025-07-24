using NewAnalyticsServcie.Application.DTOs;

namespace NewAnalyticsServcie.Application.Interfaces
{
    public interface IStudentsRepository
    {
        public Task<IEnumerable<GetAllStudentMarksByAssignmentIdResponseDTO>> GetAllStudentMarksByAssignmentId(int assignmentId);
    }
}
