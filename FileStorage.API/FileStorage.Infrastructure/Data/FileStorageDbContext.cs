using FileStorage.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Infrastructure.Data
{
    public class FileStorageDbContext(DbContextOptions<FileStorageDbContext> options) : DbContext(options)
    {
        public DbSet<SubjectEntity> SubjectEntities { get; set; }
        public DbSet<TeacherEntity> TeacherEntities { get; set; }
        public DbSet<SubjectTopicEntity> SubjectTopicEntities { get; set; }
        public DbSet<MetirialEntity> MetirialEntities { get; set; }
    }
}
