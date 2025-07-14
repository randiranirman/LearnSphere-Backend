using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseRegistration.Infrastructure.Data
{
    public class CourseRegistrationDbcontext : DbContext
    {
        public CourseRegistrationDbcontext(DbContextOptions<CourseRegistrationDbcontext> options) : base(options)
        {
        }

        // CourseRegistration specific tables
        public DbSet<Class>? Classes { get; set; }
        public DbSet<Subject>? Subjects { get; set; }
        public DbSet<ClassSubject>? ClassSubjects { get; set; }
        public DbSet<StudentClassRegistration>? StudentClassRegistrations { get; set; }
        public DbSet<TeacherClassRegistration>? TeacherClassRegistrations { get; set; }
        public DbSet<StudentRegistrationSubject>? StudentRegistrationSubjects { get; set; }
        public DbSet<StudentSubject>? StudentSubjects { get; set; }
        public DbSet<TeacherSubject>? TeacherSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Class entity configuration
            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(e => e.ClassId);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Grade)
                    .IsRequired();
                entity.Property(e => e.Description)
                    .HasMaxLength(500);
                entity.Property(e => e.MaxStudents)
                    .HasDefaultValue(30);
                entity.Property(e => e.StartDate)
                    .IsRequired();
                entity.Property(e => e.EndDate)
                    .IsRequired();
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.Status)
                    .HasDefaultValue(ClassStatus.Draft);
            });

            // Subject entity configuration
            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.SubjectId);
                entity.Property(e => e.Name)
                    .IsRequired();
                entity.Property(e => e.Code)
                    .IsRequired();
                entity.Property(e => e.Description)
                    .IsRequired();
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");
            });

            // ClassSubject entity configuration (many-to-many join table)
            modelBuilder.Entity<ClassSubject>(entity =>
            {
                entity.HasKey(cs => new { cs.ClassId, cs.SubjectId });

                entity.Property(cs => cs.AssociatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                // Relationships
                entity.HasOne(cs => cs.Class)
                    .WithMany(c => c.Subjects)
                    .HasForeignKey(cs => cs.ClassId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(cs => cs.Subject)
                    .WithMany(s => s.Classes)
                    .HasForeignKey(cs => cs.SubjectId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // StudentClassRegistration entity configuration
            modelBuilder.Entity<StudentClassRegistration>(entity =>
            {
                entity.HasKey(e => e.StudentRegistrationId);
                entity.Property(e => e.StudentId)
                    .IsRequired();
                entity.Property(e => e.ClassId)
                    .IsRequired();
                entity.Property(e => e.IndexNumber)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(e => e.Status)
                    .HasDefaultValue(RegistrationStatus.Pending);
                entity.Property(e => e.RegisteredAt)
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.Remarks)
                    .HasMaxLength(500);

                // Relationships
                entity.HasOne(sc => sc.Class)
                    .WithMany(c => c.StudentRegistrations)
                    .HasForeignKey(sc => sc.ClassId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // StudentRegistrationSubject entity configuration
            modelBuilder.Entity<StudentRegistrationSubject>(entity =>
            {
                entity.ToTable("StudentRegistrationSubject");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StudentRegistrationId)
                    .IsRequired();
                entity.Property(e => e.SubjectId)
                    .IsRequired();
                entity.Property(e => e.AddedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                // Relationships
                entity.HasOne(srs => srs.StudentRegistration)
                    .WithMany(sr => sr.RegistrationSubjects)
                    .HasForeignKey(srs => srs.StudentRegistrationId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(srs => srs.Subject)
                    .WithMany()
                    .HasForeignKey(srs => srs.SubjectId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // TeacherClassRegistration entity configuration
            modelBuilder.Entity<TeacherClassRegistration>(entity =>
            {
                entity.HasKey(e => e.TeacherRegistrationId);
                entity.Property(e => e.TeacherId)
                    .IsRequired();
                entity.Property(e => e.ClassId)
                    .IsRequired();
                entity.Property(e => e.SubjectId)
                    .IsRequired();
                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Status)
                    .HasDefaultValue(RegistrationStatus.Pending);
                entity.Property(e => e.RegisteredAt)
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.Remarks)
                    .HasMaxLength(500);

                // Relationships
                entity.HasOne(tc => tc.Class)
                    .WithMany(c => c.TeacherRegistrations)
                    .HasForeignKey(tc => tc.ClassId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(tc => tc.Subject)
                    .WithMany()
                    .HasForeignKey(tc => tc.SubjectId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // StudentSubject entity configuration
            modelBuilder.Entity<StudentSubject>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StudentId)
                    .IsRequired();
                entity.Property(e => e.SubjectId)
                    .IsRequired();
                entity.Property(e => e.EnrolledAt)
                            .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true);

                // Relationship
                entity.HasOne(ss => ss.Subject)
                    .WithMany(s => s.StudentSubjects)
                    .HasForeignKey(ss => ss.SubjectId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // TeacherSubject entity configuration
            modelBuilder.Entity<TeacherSubject>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TeacherId)
                    .IsRequired();
                entity.Property(e => e.SubjectId)
                    .IsRequired();
                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Status)
                    .HasDefaultValue(RegistrationStatus.Pending);
                entity.Property(e => e.RegisteredAt)
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.Remarks)
                    .HasMaxLength(500);
                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true);

                // Relationship
                entity.HasOne(ts => ts.Subject)
                    .WithMany(s => s.TeacherSubjects)
                    .HasForeignKey(ts => ts.SubjectId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }


    }

    }

