using AnalyticsService.Application.DTOs;

namespace AnalyticsService.Application.Interfaces
{
    public interface IAssignmentsRepository
    {
        public Task<IEnumerable<AssignmentDTO>> GetAllAssignmentsBySubjectIdAsync(int subjectId);
        public Task<IEnumerable<StudentsByAssignmentIdResponseDTO>> GetAllStudentsByAssignmentIdAsync(int assignmentId);
    }
}
