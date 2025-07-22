using CourseRegistration.Application.Repositories;
using CourseRegistration.Domain.Models;
using CourseRegistration.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseRegistration.Infrastructure.Repositories
{
    public class StudentSubjectRepository : IStudentSubjectRepository
    {
        private readonly CourseRegistrationDbcontext _context;

        public DbContext Context => _context;

        public StudentSubjectRepository(CourseRegistrationDbcontext context)
        {
            _context = context;
        }

        public async Task<StudentSubject> AddAsync(StudentSubject entity)
        {
            _context.StudentSubjects.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.StudentSubjects.FindAsync(id);
            if (entity != null)
            {
                _context.StudentSubjects.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.StudentSubjects.AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<StudentSubject>> GetAllAsync()
        {
            return await _context.StudentSubjects
                .Include(x => x.Subject)
                .ToListAsync();
        }

        public async Task<StudentSubject?> GetByIdAsync(int id)
        {
            return await _context.StudentSubjects
                .Include(x => x.Subject)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<StudentSubject>> GetByStudentIdAsync(int studentId)
        {
            return await _context.StudentSubjects
                .Include(x => x.Subject)
                .Where(x => x.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<StudentSubject>> GetBySubjectIdAsync(int subjectId)
        {
            return await _context.StudentSubjects
                .Include(x => x.Subject)
                .Where(x => x.SubjectId == subjectId)
                .ToListAsync();
        }

        public async Task<StudentSubject?> GetByStudentAndSubjectAsync(int studentId, int subjectId)
        {
            return await _context.StudentSubjects
                .Include(x => x.Subject)
                .FirstOrDefaultAsync(x => x.StudentId == studentId && x.SubjectId == subjectId);
        }

        public async Task<bool> IsStudentEnrolledInSubjectAsync(int studentId, int subjectId)
        {
            return await _context.StudentSubjects
                .AnyAsync(x => x.StudentId == studentId && x.SubjectId == subjectId);
        }

        public async Task<StudentSubject> UpdateAsync(StudentSubject entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        
    }
}
