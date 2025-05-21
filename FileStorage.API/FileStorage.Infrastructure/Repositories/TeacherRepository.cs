using FileStorage.Domain.Entities;
using FileStorage.Domain.Interfaces;
using FileStorage.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Infrastructure.Repositories
{
    public class TeacherRepository(FileStorageDbContext fileStorageDbContext) : ITeacherRepository
    {
        public async Task<TeacherEntity?> GetTeacherByIdAsync(int Id)
        {
            var teacher = await fileStorageDbContext.TeacherEntities.FirstOrDefaultAsync(x => x.Id == Id);
            return teacher;
        }
    }
}
