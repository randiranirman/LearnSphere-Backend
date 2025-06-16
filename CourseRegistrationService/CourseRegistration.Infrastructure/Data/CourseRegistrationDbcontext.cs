using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Domain.Models;
using UserManagement.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace CourseRegistration.Infrastructure.Data
{
    public class CourseRegistrationDbcontext : DbContext
    {
        public CourseRegistrationDbcontext(DbContextOptions<CourseRegistrationDbcontext> options) : base(options)
        {
        }

        // Shared tables from UserManagement
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Teacher> Teachers { get; set; } = null!;
        
        // CourseRegistration specific tables
        public DbSet<Subject> Subjects { get; set; } = null!;
        public DbSet<Class> Classes { get; set; } = null!;
        public DbSet<TeacherSubject> TeacherSubjects { get; set; } = null!;
        public DbSet<StudentSubject> StudentSubjects { get; set; } = null!;
        public DbSet<StudentClassRegistration> StudentClassRegistrations { get; set; } = null!;
        public DbSet<TeacherClassRegistration> TeacherClassRegistrations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Subject configuration  
            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subjects");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Code).HasMaxLength(20).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.HasIndex(e => e.Code).IsUnique();

                // Configure relationships
                entity.HasMany(s => s.Classes)
                    .WithOne(c => c.Subject)
                    .HasForeignKey(c => c.SubjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(s => s.StudentSubjects)
                    .WithOne(ss => ss.Subject)
                    .HasForeignKey(ss => ss.SubjectId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Class configuration  
            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Classes");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Grade).IsRequired();
                entity.Property(e => e.MaxStudents).IsRequired();
                entity.Property(e => e.Status).IsRequired();

                // Configure navigation properties  
                entity.HasMany(c => c.StudentRegistrations)
                    .WithOne(sr => sr.Class)
                    .HasForeignKey(sr => sr.ClassId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(c => c.TeacherRegistrations)
                    .WithOne(tr => tr.Class)
                    .HasForeignKey(tr => tr.ClassId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Student configuration
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Students");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.IndexNumber).HasMaxLength(20).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
                entity.Property(e => e.ContactNumber).HasMaxLength(15).IsRequired();
                entity.Property(e => e.Address).HasMaxLength(200).IsRequired();
                entity.Property(e => e.ParentContactNumber).HasMaxLength(15);
                entity.Property(e => e.ParentName).HasMaxLength(100);
                
                // Unique constraints
                entity.HasIndex(e => e.IndexNumber).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Teacher configuration
            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teachers");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
                entity.Property(e => e.ContactNumber).HasMaxLength(15).IsRequired();
                entity.Property(e => e.Address).HasMaxLength(200).IsRequired();
                entity.Property(e => e.EmployeeId).HasMaxLength(50);
                entity.Property(e => e.Qualification).HasMaxLength(100);
                
                // Unique constraints
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.EmployeeId).IsUnique().HasFilter("[EmployeeId] IS NOT NULL");
            });

            // TeacherSubject configuration  
            modelBuilder.Entity<TeacherSubject>(entity =>
            {
                entity.ToTable("TeacherSubjects");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TeacherId).IsRequired();
                entity.Property(e => e.SubjectId).IsRequired();
                entity.Property(e => e.IsActive).IsRequired();

                // Configure relationships
                entity.HasOne(ts => ts.Teacher)
                    .WithMany()
                    .HasForeignKey(ts => ts.TeacherId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ts => ts.Subject)
                    .WithMany(s => s.TeacherSubjects)
                    .HasForeignKey(ts => ts.SubjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Create composite unique index to prevent duplicate assignments  
                entity.HasIndex(e => new { e.TeacherId, e.SubjectId }).IsUnique()
                    .HasDatabaseName("IX_TeacherSubject_UniqueAssignment");
            });

            // StudentSubject configuration  
            modelBuilder.Entity<StudentSubject>(entity =>
            {
                entity.ToTable("StudentSubjects");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StudentId).IsRequired();
                entity.Property(e => e.SubjectId).IsRequired();
                entity.Property(e => e.IsActive).HasDefaultValue(true);

                // Configure relationships
                entity.HasOne(ss => ss.Student)
                    .WithMany()
                    .HasForeignKey(ss => ss.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ss => ss.Subject)
                    .WithMany(s => s.StudentSubjects)
                    .HasForeignKey(ss => ss.SubjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Create composite unique index to prevent duplicate enrollment  
                entity.HasIndex(e => new { e.StudentId, e.SubjectId }).IsUnique()
                    .HasDatabaseName("IX_StudentSubjects_StudentId_SubjectId");
            });

            // StudentClassRegistration configuration
            modelBuilder.Entity<StudentClassRegistration>(entity =>
            {
                entity.ToTable("StudentClassRegistrations");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StudentId).IsRequired();
                entity.Property(e => e.ClassId).IsRequired();
                entity.Property(e => e.SubjectId).IsRequired();
                entity.Property(e => e.IndexNumber).HasMaxLength(20).IsRequired();
                entity.Property(e => e.Status).HasConversion<int>().HasDefaultValue(RegistrationStatus.Pending);
                entity.Property(e => e.Remarks).HasMaxLength(500);

                // Configure relationships
                entity.HasOne(scr => scr.Student)
                    .WithMany()
                    .HasForeignKey(scr => scr.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(scr => scr.Class)
                    .WithMany(c => c.StudentRegistrations)
                    .HasForeignKey(scr => scr.ClassId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(scr => scr.Subject)
                    .WithMany()
                    .HasForeignKey(scr => scr.SubjectId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Create composite unique index to prevent duplicate registrations
                entity.HasIndex(e => new { e.StudentId, e.ClassId })
                    .IsUnique()
                    .HasDatabaseName("IX_StudentClassRegistrations_StudentId_ClassId");
            });

            // TeacherClassRegistration configuration  
            modelBuilder.Entity<TeacherClassRegistration>(entity =>
            {
                entity.ToTable("TeacherClassRegistrations");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TeacherId).IsRequired();
                entity.Property(e => e.ClassId).IsRequired();
                entity.Property(e => e.SubjectId).IsRequired();
                entity.Property(e => e.EmployeeId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Status).HasConversion<int>().HasDefaultValue(RegistrationStatus.Pending);
                entity.Property(e => e.Remarks).HasMaxLength(500);

                // Configure relationships
                entity.HasOne(tcr => tcr.Teacher)
                    .WithMany()
                    .HasForeignKey(tcr => tcr.TeacherId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(tcr => tcr.Class)
                    .WithMany(c => c.TeacherRegistrations)
                    .HasForeignKey(tcr => tcr.ClassId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(tcr => tcr.Subject)
                    .WithMany(s => s.TeacherClassRegistrations)
                    .HasForeignKey(tcr => tcr.SubjectId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Create composite unique index to prevent duplicate registrations  
                entity.HasIndex(e => new { e.TeacherId, e.ClassId })
                    .IsUnique()
                    .HasDatabaseName("IX_TeacherClassRegistrations_TeacherId_ClassId");
            });

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Use fixed DateTime values instead of DateTime.UtcNow for seeding
            var seedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<Subject>().HasData(
                new Subject
                {
                    Id = 1,
                    Name = "Mathematics",
                    Code = "MATH101",
                    Description = "Basic Mathematics",
                    CreatedAt = seedDate
                },
                new Subject
                {
                    Id = 2,
                    Name = "Science",
                    Code = "SCI101",
                    Description = "Basic Science",
                    CreatedAt = seedDate
                },
                new Subject
                {
                    Id = 3,
                    Name = "History",
                    Code = "HIST101",
                    Description = "World History",
                    CreatedAt = seedDate
                }
            );
        }
    }
}