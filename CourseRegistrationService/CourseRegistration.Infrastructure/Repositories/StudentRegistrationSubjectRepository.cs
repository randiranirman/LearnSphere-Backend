using CourseRegistration.Application.Repositories;
using CourseRegistration.Domain.Models;
using CourseRegistration.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseRegistration.Infrastructure.Repositories
{
    public class StudentRegistrationSubjectRepository : IStudentRegistrationSubjectRepository
    {
        private readonly CourseRegistrationDbcontext _context;

        public StudentRegistrationSubjectRepository(CourseRegistrationDbcontext context)
        {
            _context = context;
        }

        public async Task<StudentRegistrationSubject> AddAsync(StudentRegistrationSubject entity)
        {
            _context.StudentRegistrationSubjects.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.StudentRegistrationSubjects.FindAsync(id);
            if (entity != null)
            {
                _context.StudentRegistrationSubjects.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteByRegistrationIdAsync(int registrationId)
        {
            var entities = await _context.StudentRegistrationSubjects
                .Where(x => x.StudentRegistrationId == registrationId)
                .ToListAsync();
            
            if (entities.Any())
            {
                _context.StudentRegistrationSubjects.RemoveRange(entities);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.StudentRegistrationSubjects.AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<StudentRegistrationSubject>> GetAllAsync()
        {
            return await _context.StudentRegistrationSubjects
                .Include(x => x.Subject)
                .Include(x => x.StudentRegistration)
                .ToListAsync();
        }

        public async Task<StudentRegistrationSubject?> GetByIdAsync(int id)
        {
            return await _context.StudentRegistrationSubjects
                .Include(x => x.Subject)
                .Include(x => x.StudentRegistration)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<StudentRegistrationSubject>> GetByRegistrationIdAsync(int registrationId)
        {
            return await _context.StudentRegistrationSubjects
                .Include(x => x.Subject)
                .Where(x => x.StudentRegistrationId == registrationId)
                .ToListAsync();
        }

        public async Task<IEnumerable<StudentRegistrationSubject>> GetBySubjectIdAsync(int subjectId)
        {
            return await _context.StudentRegistrationSubjects
                .Include(x => x.Subject)
                .Include(x => x.StudentRegistration)
                .Where(x => x.SubjectId == subjectId)
                .ToListAsync();
        }

        public async Task<StudentRegistrationSubject?> GetByRegistrationAndSubjectAsync(int registrationId, int subjectId)
        {
            return await _context.StudentRegistrationSubjects
                .Include(x => x.Subject)
                .Include(x => x.StudentRegistration)
                .FirstOrDefaultAsync(x => x.StudentRegistrationId == registrationId && x.SubjectId == subjectId);
        }

        public async Task<StudentRegistrationSubject> UpdateAsync(StudentRegistrationSubject entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
