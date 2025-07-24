using NewAnalyticsService.Application.DTOs;

namespace NewAnalyticsService.Application.Interfaces
{
    public interface IAnalyticsRepository
    {
        public Task<List<GetAllSubjectsWithAssignmentsAndSubmissionsByTeacherIdResponseDTO>> GetAllSubjectsWithAssignmentsAndSubmissionsByTeacherId(int teacherId);

        public Task<IEnumerable<GetAllAssignmentMarksByStudentIdAndSubjectIdResponseDTO>> GetAllAssignmentMarksByStudentIdAndSubjectId(int studentId, int subjectId);
    }
}