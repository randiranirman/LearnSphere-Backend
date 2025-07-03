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
    public class SubjectRepository<T> : BaseRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(CourseRegistrationDbcontext context) : base(context)
        {
        }


        public async Task<Subject?> GetByCodeAsync(string code)
        {
            return await _set.FirstOrDefaultAsync(s => s.Code == code);
        }

        public async Task<IEnumerable<Subject>> GetSubjectByStudentIdAsync(int studentId)
        {
            return await _context.StudentSubjects.Where(ss => ss.StudentId == studentId && ss.IsActive).Include(ss => ss.Subject).Select(ss => ss.Subject)
                  .ToListAsync();
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByGradeIdAsync(int grade)
        {
            return await _context.Classes.Where(c => c.Grade == grade).Include
                (c => c.Subject).Select(c => c.Subject).Distinct().ToListAsync();
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByTeacherIdAsync(int teacherId)
        {
            return await _context.TeacherSubjects
                .Where(ts => ts.TeacherID == teacherId && ts.IsActive)
                .Include(ts => ts.Subject)
                .Select(ts => ts.Subject!) // Use null-forgiving operator to ensure Subject is not null  
                .ToListAsync();
        }

        public override async  Task<Subject?> GetByIdAsync(int id)
        {
            return await _set
               .Include(s => s.TeacherSubjects)
               .Include(s => s.StudentSubjects)
               .Include(s => s.Classes)
               .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
