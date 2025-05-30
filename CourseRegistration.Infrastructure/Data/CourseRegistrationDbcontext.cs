﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseRegistration.Infrastructure.Data
{
    public class CourseRegistrationDbcontext(DbContextOptions<CourseRegistrationDbcontext> options) : DbContext(options) // Fixed constructor syntax  
    {
        

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

                // Configure navigation properties  
                entity.HasMany(s => s.Classes)
                    .WithOne(c => c.Subject)
                    .HasForeignKey(c => c.SubjectId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
