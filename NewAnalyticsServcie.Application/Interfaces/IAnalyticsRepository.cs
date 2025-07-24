using NewAnalyticsServcie.Application.DTOs;

namespace NewAnalyticsServcie.Application.Interfaces
{
    public interface IAnalyticsRepository
    {
        public Task<List<GetAllSubjectsWithAssignmentsAndSubmissionsByTeacherIdResponseDTO>> GetAllSubjectsWithAssignmentsAndSubmissionsByTeacherId(int teacherId);

        public Task<IEnumerable<GetAllAssignmentMarksByStudentIdAndSubjectIdResponseDTO>> GetAllAssignmentMarksByStudentIdAndSubjectId(int studentId, int subjectId);
    }
}
