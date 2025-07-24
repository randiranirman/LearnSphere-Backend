using NewAnalyticsService.Application.DTOs;

namespace NewAnalyticsService.Application.Interfaces
{
    public interface ITeacherSubjectHttpService
    {
        public Task<List<SubjectsByTeacherIdDTO>> GetSubjectsByTeacherIdAsync(int teacherId);
        public Task<int> GetRegisteredStudentCountBySubjectIdAsync(int subjectId);
    }
}
