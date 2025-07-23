using AnalyticsService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnalyticsService.Infrastructure.Data
{
    public class AnalyticsDbContext(DbContextOptions<AnalyticsDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectTopic> SubjectTopics { get; set; }
        public DbSet<Metirial> Metirials { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<StudentMarks> StudentMarks { get; set; }
        public DbSet<Submission> Submissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure composite primary key for StudentMarks
            modelBuilder.Entity<StudentMarks>()
                .HasKey(sm => new { sm.AssignmentId, sm.StudentId });

            // Configure Submission entity relationships
            modelBuilder.Entity<Submission>()
                .HasOne(s => s.Assignment)
                .WithMany(a => a.Submissions)
                .HasForeignKey(s => s.AssignmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Submission>()
                .HasOne(s => s.Student)
                .WithMany(st => st.Submissions)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure properties for Submission
            modelBuilder.Entity<Submission>()
                .Property(s => s.UploadLink)
                .IsRequired()
                .HasMaxLength(500);

            modelBuilder.Entity<Submission>()
                .Property(s => s.SubmissionName)
                .IsRequired()
                .HasMaxLength(255);

            base.OnModelCreating(modelBuilder);
        }
    }
}
