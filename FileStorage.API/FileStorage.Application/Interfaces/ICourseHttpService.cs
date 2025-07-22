using FileStorage.Application.DTOs;

namespace FileStorage.Application.Interfaces
{
    public interface ICourseHttpService
    {
        public Task<IEnumerable<SubjectsDTO>> GetSubjectsByTeacherIdAsync(int teacherId);
    }
}
