using NewAnalyticsService.Application.DTOs;

namespace NewAnalyticsService.Application.Interfaces
{
    public interface IStudentsRepository
    {
        public Task<IEnumerable<GetAllStudentMarksByAssignmentIdResponseDTO>> GetAllStudentMarksByAssignmentId(int assignmentId);
    }
}
