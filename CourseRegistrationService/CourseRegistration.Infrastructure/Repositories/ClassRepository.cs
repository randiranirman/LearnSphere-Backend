using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Application.Repositories;
using CourseRegistration.Domain.Models;
using CourseRegistration.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseRegistration.Infrastructure.Repositories
{
    public class ClassRepository : BaseRepository<Class>, IClassRepository
    {
        public ClassRepository(CourseRegistrationDbcontext context) : base(context)
        {
        }

        // class repository implementation  
        public async Task<IEnumerable<Class>> GetClassesByGradeAsync(int grade)
        {
            return await _set.Where(c => c.Grade == grade).Include( c=> c.Subject).ToListAsync();
        }

        public async  Task<IEnumerable<Class>> GetClassesByStatusAsync(ClassStatus status)
        {
            return await _set
                .Where(c => c.Status == status)
                .Include(c => c.Subject)
                .ToListAsync();
        }

        public async Task<IEnumerable<Class>> GetClassesByStudentIdAsync(int studentId)
        {
            return await _context.StudentClassRegistrations.Where(scr => scr.StudentId == studentId && scr.Status== RegistrationStatus.Approved)
                .Include(scr => scr.Class)
                .ThenInclude(c => c.Subject)
                .Select(scr => scr.Class)
                .ToListAsync();
        }

        public async Task<IEnumerable<Class>> GetClassesBySubjectIdAsync(int subjectId)
        {
           return await _set
                .Where(c => c.SubjectId == subjectId)
                .Include(c => c.Subject)
                .ToListAsync();
        }

        public async Task<IEnumerable<Class>> GetClassesByTeacherIdAsync(int teacherId)
        {
            return await _context.TeacherClassRegistrations
                .Where(tcr => tcr.TeacherId == teacherId && tcr.Status == RegistrationStatus.Approved)
                .Include(tcr => tcr.Class)
                .ThenInclude(c => c.Subject)
                .Select(tcr => tcr.Class)
                .ToListAsync();
        }

        public async Task<Class?> GetClassWithRegistrationsAsync(int classId)
        {
            return await _set
                .Include(c => c.Subject)
                .Include(c => c.StudentRegistrations)
                .Include(c => c.TeacherRegistrations)
                .FirstOrDefaultAsync(c => c.Id == classId);
        }
    }
}
