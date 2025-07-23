using AnalyticsService.Application.DTOs;

namespace AnalyticsService.Application.Interfaces
{
    public interface ITeacherRepository
    {
        public Task<IEnumerable<AllSubjectsByTeacherIdDTO>> GetAllSubjectsByTeacherIdAsync(int teacherId);
    }
}
