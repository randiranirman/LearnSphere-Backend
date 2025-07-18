using FileStorage.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Infrastructure.Data
{
    public class FileStorageDbContext(DbContextOptions<FileStorageDbContext> options) : DbContext(options)
    {
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<SubjectTopic> SubjectTopics { get; set; }
        public DbSet<Submission> Submissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assignment>().ToTable("Assignment");
            modelBuilder.Entity<Material>().ToTable("Material");
            modelBuilder.Entity<SubjectTopic>().ToTable("SubjectTopic");
            modelBuilder.Entity<Submission>().ToTable("Submission");

            // Assignment ↔ Submission: One-to-many
            modelBuilder.Entity<Assignment>()
                .HasMany(a => a.Submissions)
                .WithOne(s => s.Assignment)
                .HasForeignKey(s => s.AssignmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // SubjectTopic ↔ Material: One-to-many
            modelBuilder.Entity<Material>()
                .HasOne(m => m.SubjectTopic)
                .WithMany(st => st.Materials)
                .HasForeignKey(m => m.TopicId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
