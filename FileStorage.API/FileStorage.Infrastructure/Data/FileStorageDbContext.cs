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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // put our configurations
            modelBuilder.Entity<TeacherEntity>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.FullName)
                    .IsRequired();
                entity.HasMany(a => a.AssigedSubjects)
                    .WithOne(a => a.AssignedTeacher)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<SubjectEntity>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Code)
                    .IsRequired();
                entity.HasOne(a => a.AssignedTeacher)
                    .WithMany(a => a.AssigedSubjects)
                    .HasForeignKey(a => a.AssignedTeacherId)
                    .OnDelete(DeleteBehavior.NoAction);
                entity.HasMany(a => a.SubjectTopics)
                    .WithOne(a => a.Subject)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<SubjectTopicEntity>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.TopicName)
                    .IsRequired();
                entity.HasOne(a => a.Subject)
                    .WithMany(a => a.SubjectTopics)
                    .HasForeignKey(a => a.SubjectId)
                    .OnDelete(DeleteBehavior.NoAction);
                entity.HasMany(a => a.Metirials)
                    .WithOne(a => a.SubjectTopic)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<MetirialEntity>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.UploadLink)
                    .IsRequired();
                entity.Property(a => a.FileType)
                    .IsRequired();
                entity.HasOne(a => a.SubjectTopic)
                    .WithMany(a => a.Metirials)
                    .HasForeignKey(a => a.TopicId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

        }
    }
}
