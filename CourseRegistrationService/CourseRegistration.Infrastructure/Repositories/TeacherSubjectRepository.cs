using CourseRegistration.Application.Repositories;
using CourseRegistration.Domain.Models;
using CourseRegistration.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseRegistration.Infrastructure.Repositories
{
    public class TeacherSubjectRepository : ITeacherSubjectRepository
    {
        private readonly CourseRegistrationDbcontext _context;

        public DbContext Context => _context;

        public TeacherSubjectRepository(CourseRegistrationDbcontext context)
        {
            _context = context;
        }

        public async Task<TeacherSubject> AddAsync(TeacherSubject entity)
        {
            _context.TeacherSubjects?.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TeacherSubject?> GetByIdAsync(int id)
        {
            return await _context.TeacherSubjects?
                .Include(t => t.Subject)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TeacherSubject>> GetAllAsync()
        {
            return await _context.TeacherSubjects?
                .Include(t => t.Subject)
                .ToListAsync() ?? new List<TeacherSubject>();
        }

        public async Task<TeacherSubject> UpdateAsync(TeacherSubject entity)
        {
            _context.TeacherSubjects?.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.TeacherSubjects?
                .FirstOrDefaultAsync(t => t.Id == id);
            if (entity != null)
            {
                _context.TeacherSubjects?.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistAsync(int id)
        {
            return _context.TeacherSubjects != null && await _context.TeacherSubjects
                .AnyAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TeacherSubject>> GetByTeacherIdAsync(int teacherId)
        {
            return await _context.TeacherSubjects?
                .Include(t => t.Subject)
                .Where(t => t.TeacherId == teacherId)
                .ToListAsync() ?? new List<TeacherSubject>();
        }

        public async Task<IEnumerable<TeacherSubject>> GetBySubjectIdAsync(int subjectId)
        {
            return await _context.TeacherSubjects?
                .Include(t => t.Subject)
                .Where(t => t.SubjectId == subjectId)
                .ToListAsync() ?? new List<TeacherSubject>();
        }

        public async Task<TeacherSubject?> GetByTeacherAndSubjectAsync(int teacherId, int subjectId)
        {
            return await _context.TeacherSubjects?
                .Include(t => t.Subject)
                .FirstOrDefaultAsync(t => t.TeacherId == teacherId && t.SubjectId == subjectId);
        }

        public async Task<bool> IsTeacherAssignedToSubjectAsync(int teacherId, int subjectId)
        {
            return _context.TeacherSubjects != null && await _context.TeacherSubjects
                .AnyAsync(t => t.TeacherId == teacherId && t.SubjectId == subjectId && t.IsActive);
        }

        public async Task<IEnumerable<TeacherSubject>> GetByStatusAsync(RegistrationStatus status)
        {
            return await _context.TeacherSubjects?
                .Include(t => t.Subject)
                .Where(t => t.Status == status)
                .ToListAsync() ?? new List<TeacherSubject>();
        }
    }
}
