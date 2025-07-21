using CourseRegistration.Application.Repositories;
using CourseRegistration.Domain.Models;
using CourseRegistration.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseRegistration.Infrastructure.Repositories
{
    public class StudentClassRegistrationRepository : IStudentClassRegistrationRepository
    {
        private readonly CourseRegistrationDbcontext _context;

        public DbContext Context => _context;

        public StudentClassRegistrationRepository(CourseRegistrationDbcontext context)
        {
            _context = context;
        }

        public async Task<StudentClassRegistration> AddAsync(StudentClassRegistration entity)
        {
            _context.StudentClassRegistrations.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.StudentClassRegistrations.FindAsync(id);
            if (entity != null)
            {
                _context.StudentClassRegistrations.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.StudentClassRegistrations.AnyAsync(x => x.StudentRegistrationId == id);
        }

        public async Task<IEnumerable<StudentClassRegistration>> GetAllAsync()
        {
            return await _context.StudentClassRegistrations
                .Include(x => x.Class)
                .Include(x => x.RegistrationSubjects)
                    .ThenInclude(rs => rs.Subject)
                .ToListAsync();
        }

        public async Task<StudentClassRegistration?> GetByIdAsync(int id)
        {
            return await _context.StudentClassRegistrations
                .Include(x => x.Class)
                .Include(x => x.RegistrationSubjects)
                    .ThenInclude(rs => rs.Subject)
                .FirstOrDefaultAsync(x => x.StudentRegistrationId == id);
        }

        public async Task<IEnumerable<StudentClassRegistration>> GetByStudentIdAsync(int studentId)
        {
            return await _context.StudentClassRegistrations
                .Include(x => x.Class)
                .Include(x => x.RegistrationSubjects)
                    .ThenInclude(rs => rs.Subject)
                .Where(x => x.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<StudentClassRegistration>> GetByClassIdAsync(int classId)
        {
            return await _context.StudentClassRegistrations
                .Include(x => x.Class)
                .Include(x => x.RegistrationSubjects)
                    .ThenInclude(rs => rs.Subject)
                .Where(x => x.ClassId == classId)
                .ToListAsync();
        }

        public async Task<IEnumerable<StudentClassRegistration>> GetByStatusAsync(RegistrationStatus status)
        {
            return await _context.StudentClassRegistrations
                .Include(x => x.Class)
                .Include(x => x.RegistrationSubjects)
                    .ThenInclude(rs => rs.Subject)
                .Where(x => x.Status == status)
                .ToListAsync();
        }

        public async Task<StudentClassRegistration?> GetByStudentAndClassAsync(int studentId, int classId)
        {
            return await _context.StudentClassRegistrations
                .Include(x => x.Class)
                .Include(x => x.RegistrationSubjects)
                    .ThenInclude(rs => rs.Subject)
                .FirstOrDefaultAsync(x => x.StudentId == studentId && x.ClassId == classId);
        }

        public async Task<IEnumerable<StudentClassRegistration>> GetPendingRegistrationsAsync()
        {
            return await _context.StudentClassRegistrations
                .Include(x => x.Class)
                .Include(x => x.RegistrationSubjects)
                    .ThenInclude(rs => rs.Subject)
                .Where(x => x.Status == RegistrationStatus.Pending)
                .ToListAsync();
        }
        async Task<IEnumerable<StudentClassRegistration>> IStudentClassRegistrationRepository.GetApprovedRegistrationsAsync()
        {
            return await _context.StudentClassRegistrations.Include(x => x.Class)
                .Include(x => x.RegistrationSubjects)
                .ThenInclude(rs => rs.Subject)  
                .Where( x => x.Status== RegistrationStatus.Approved)
                .ToListAsync();
        }

        public async Task<int> GetRegisteredStudentCountAsync(int classId)
        {
            return await _context.StudentClassRegistrations
                .CountAsync(x => x.ClassId == classId && x.Status == RegistrationStatus.Approved);
        }

        public async Task<StudentClassRegistration> UpdateAsync(StudentClassRegistration entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        
    }
}
