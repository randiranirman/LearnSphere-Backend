using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseRegistration.Application.Repositories;
using CourseRegistration.Domain.Models;
using CourseRegistration.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseRegistration.Infrastructure.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly CourseRegistrationDbcontext _context;

        public DbContext Context => _context;

        public ClassRepository(CourseRegistrationDbcontext context)
        {
            _context = context;
        }

        public async Task<Class> AddAsync(Class entity)
        {
            _context.Classes.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Classes.FindAsync(id);
            if (entity != null)
            {
                _context.Classes.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Classes.AnyAsync(x => x.ClassId == id);
        }

        public async Task<IEnumerable<Class>> GetAllAsync()
        {
            return await _context.Classes.ToListAsync();
        }

        public async Task<Class?> GetByIdAsync(int id)
        {
            return await _context.Classes.FirstOrDefaultAsync(x => x.ClassId == id);
        }

        public async Task<IEnumerable<Class>> GetClassesBySubjectIdAsync(int subjectId)
        {
            return await _context.Classes
                .Include(c => c.Subjects)
                .Where(c => c.Subjects.Any(cs => cs.SubjectId == subjectId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Class>> GetClassesByGradeAsync(int grade)
        {
            return await _context.Classes
                .Where(c => c.Grade == grade)
                .ToListAsync();
        }

        public async Task<IEnumerable<Class>> GetClassesByStatusAsync(ClassStatus status)
        {
            return await _context.Classes
                .Where(c => c.Status == status)
                .ToListAsync();
        }

        public async Task<Class?> GetClassWithRegistrationsAsync(int classId)
        {
            return await _context.Classes
                .Include(c => c.StudentRegistrations)
                .FirstOrDefaultAsync(c => c.ClassId == classId);
        }

        public async Task<IEnumerable<Class>> GetClassesByTeacherIdAsync(int teacherId)
        {
            return await _context.Classes
                .Include(c => c.TeacherRegistrations)
                .Where(c => c.TeacherRegistrations.Any(tcr => tcr.TeacherId == teacherId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Class>> GetClassesByStudentIdAsync(int studentId)
        {
            return await _context.Classes
                .Include(c => c.StudentRegistrations)
                .Where(c => c.StudentRegistrations.Any(scr => scr.StudentId == studentId))
                .ToListAsync();
        }

        public async Task<Class> UpdateAsync(Class entity)
        {
           try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return entity;
            }catch( Exception e )
            {
                return null;
            }
        }

        async Task IRepository<Class>.DeleteAsync(int id)
        {
            var entity = await _context.Classes.FindAsync(id);
            if (entity != null)
            {
                _context.Classes.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}