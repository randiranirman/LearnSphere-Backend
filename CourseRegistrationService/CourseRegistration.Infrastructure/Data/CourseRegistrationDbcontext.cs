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
        public DbSet<Subject> Subjects { get; set; } = null!;
        public DbSet<Class> Classes { get; set; } = null!;
        public DbSet<TeacherSubject> TeacherSubjects { get; set; } = null!;
        public DbSet<StudentSubject> StudentSubjects { get; set; } = null!;
        public DbSet<StudentClassRegistration> StudentClassRegistrations { get; set; } = null!;
        public DbSet<TeacherClassRegistration> TeacherClassRegistrations { get; set; } = null!;
    }
}
