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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure composite primary key for StudentMarks
            modelBuilder.Entity<StudentMarks>()
                .HasKey(sm => new { sm.AssignmentId, sm.StudentId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
