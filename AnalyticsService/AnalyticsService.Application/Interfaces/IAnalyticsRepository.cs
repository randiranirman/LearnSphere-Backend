using AnalyticsService.Application.DTOs;

namespace AnalyticsService.Application.Interfaces
{
    public interface IAnalyticsRepository
    {
        public Task<GetOverviewDTO> GetNoOfStudentsAndNoOfTeacher();
        public Task<IEnumerable<TeachersWithSubjectsCountDTO>> GetTeachersWithSubjectsCounts();
        public Task<IEnumerable<SubejctsWithRegisteredStudentsCountDTO>> GetTeacherSubejctsWithStudentCount(int teacherId);
        public Task<IEnumerable<StudentDetailsResponseDTO>> GetAllStudentsRegistered();
        public Task<StudentDetailsResponseDTO> GetStudentByIndexNo(string IndexNo);
    }
}
