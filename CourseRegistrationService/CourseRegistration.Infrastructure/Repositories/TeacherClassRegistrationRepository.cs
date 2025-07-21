using CourseRegistration.Application.Repositories;
using CourseRegistration.Domain.Models;
using CourseRegistration.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseRegistration.Infrastructure.Repositories
{
    public class TeacherClassRegistrationRepository : ITeacherClassRegistrationRepository
    {
        private readonly CourseRegistrationDbcontext _context;

        public DbContext Context => _context;

        public TeacherClassRegistrationRepository(CourseRegistrationDbcontext context)
        {
            _context = context;
        }

        public async Task<TeacherClassRegistration> AddAsync(TeacherClassRegistration entity)
        {
            _context.TeacherClassRegistrations?.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TeacherClassRegistration?> GetByIdAsync(int id)
        {
            return await _context.TeacherClassRegistrations?
                .Include(t => t.Class)
                .Include(t => t.Subject)
                .FirstOrDefaultAsync(t => t.TeacherRegistrationId == id);
        }

        public async Task<IEnumerable<TeacherClassRegistration>> GetAllAsync()
        {
            return await _context.TeacherClassRegistrations?
                .Include(t => t.Class)
                .Include(t => t.Subject)
                .ToListAsync() ?? new List<TeacherClassRegistration>();
        }

        public async Task<TeacherClassRegistration> UpdateAsync(TeacherClassRegistration entity)
        {
            _context.TeacherClassRegistrations?.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.TeacherClassRegistrations?
                .FirstOrDefaultAsync(t => t.TeacherRegistrationId == id);
            if (entity != null)
            {
                _context.TeacherClassRegistrations?.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistAsync(int id)
        {
            return _context.TeacherClassRegistrations != null && await _context.TeacherClassRegistrations
                .AnyAsync(t => t.TeacherRegistrationId == id);
        }

        public async Task<IEnumerable<TeacherClassRegistration>> GetByTeacherIdAsync(int teacherId)
        {
            return await _context.TeacherClassRegistrations?
                .Include(t => t.Class)
                .Include(t => t.Subject)
                .Where(t => t.TeacherId == teacherId)
                .ToListAsync() ?? new List<TeacherClassRegistration>();
        }

        public async Task<IEnumerable<TeacherClassRegistration>> GetByClassIdAsync(int classId)
        {
            return await _context.TeacherClassRegistrations?
                .Include(t => t.Class)
                .Include(t => t.Subject)
                .Where(t => t.ClassId == classId)
                .ToListAsync() ?? new List<TeacherClassRegistration>();
        }

        public async Task<IEnumerable<TeacherClassRegistration>> GetByStatusAsync(RegistrationStatus status)
        {
            return await _context.TeacherClassRegistrations?
                .Include(t => t.Class)
                .Include(t => t.Subject)
                .Where(t => t.Status == status)
                .ToListAsync() ?? new List<TeacherClassRegistration>();
        }

        public async Task<TeacherClassRegistration?> GetByTeacherAndClassAsync(int teacherId, int classId)
        {
            return await _context.TeacherClassRegistrations?
                .Include(t => t.Class)
                .Include(t => t.Subject)
                .FirstOrDefaultAsync(t => t.TeacherId == teacherId && t.ClassId == classId);
        }

        public async Task<IEnumerable<TeacherClassRegistration>> GetPendingRegistrationsAsync()
        {
            return await _context.TeacherClassRegistrations?
                .Include(t => t.Class)
                .Include(t => t.Subject)
                .Where(t => t.Status == RegistrationStatus.Pending)
                .OrderBy(t => t.RegisteredAt)
                .ToListAsync() ?? new List<TeacherClassRegistration>();
        }
    }
}
