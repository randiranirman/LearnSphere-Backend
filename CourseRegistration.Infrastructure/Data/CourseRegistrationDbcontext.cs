using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace CourseRegistration.Infrastructure.Data
{
    public class CourseRegistrationDbcontext : DbContext // Fixed constructor syntax  
    {
        public CourseRegistrationDbcontext(DbContextOptions<CourseRegistrationDbcontext> options) : base(options)
        {
        }

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

                entity.HasMany(s => s.Classes)
                .WithOne(c => c.Subject)
                .HasForeignKey(c => c.SubjectId)
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

            // TeacherSubject configuration  
            modelBuilder.Entity<TeacherSubject>(entity =>
            {
                entity.ToTable("TeacherSubjects");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TeacherID).IsRequired();
                entity.Property(e => e.SubjectID).IsRequired();
                entity.Property(e => e.IsActive).IsRequired();

                // Create composite unique index to prevent duplicate assignments  
                entity.HasIndex(e => new { e.TeacherID, e.SubjectID }).IsUnique()
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

                // Create composite unique index to prevent duplicate enrollment  
                entity.HasIndex(e => new { e.StudentId, e.SubjectId }).IsUnique()
                .HasDatabaseName("IX_StudentSubjects_StudentId_SubjectId");
            });

            // TeacherClassRegistration configuration  
            modelBuilder.Entity<TeacherClassRegistration>(entity =>
            {
                entity.ToTable("TeacherClassRegistrations");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TeacherId).IsRequired();
                entity.Property(e => e.ClassId).IsRequired();
                entity.Property(e => e.Status).HasConversion<int>().HasDefaultValue(RegistrationStatus.Pending);
                entity.Property(e => e.Remarks).HasMaxLength(500);

                // Create composite unique index to prevent duplicate registrations  
                entity.HasIndex(e => new { e.TeacherId, e.ClassId })
                    .IsUnique()
                    .HasDatabaseName("IX_TeacherClassRegistrations_TeacherId_ClassId");
            });
           SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>().HasData(
                new Subject
                {
                    Id = 1,
                    Name = "Mathematics",
                    Code = "MATH101",
                    Description = "Basic Mathematics",
                    CreatedAt = DateTime.UtcNow
                },
                new Subject
                {
                    Id = 2,
                    Name = "Science",
                    Code = "SCI101",
                    Description = "Basic Science",
                    CreatedAt = DateTime.UtcNow
                },
                new Subject
                {
                    Id = 3,
                    Name = "History",
                    Code = "HIST101",
                    Description = "World History",
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
    }

