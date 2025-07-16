using AnalyticsService.Application.DTOs;

namespace AnalyticsService.Application.Interfaces
{
    public interface IAnalyticsRepository
    {
        public Task<GetOverviewDTO> GetNoOfStudentsAndNoOfTeacher();
        public Task<IEnumerable<TeachersWithSubjectsCountDTO>> GetTeachersWithSubjectsCounts();
        public Task<IEnumerable<SubejctsWithRegisteredStudentsCountDTO>> GetTeacherSubejctsWithStudentCount(int teacherId);
    }
}
