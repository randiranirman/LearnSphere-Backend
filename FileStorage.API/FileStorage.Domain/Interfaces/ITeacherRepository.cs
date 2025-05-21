using FileStorage.Domain.Entities;

namespace FileStorage.Domain.Interfaces
{
    public interface ITeacherRepository
    {
        public Task<TeacherEntity?> GetTeacherByIdAsync(int Id);
    }
}
