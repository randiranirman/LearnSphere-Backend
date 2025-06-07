using FileStorage.Domain.Entities;

namespace FileStorage.Application.Interfaces
{
    public interface ITeacherRepository
    {
        public Task<TeacherEntity?> GetTeacherByIdAsync(int Id);
    }
}
