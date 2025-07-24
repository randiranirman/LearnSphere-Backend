using CourseRegistration.Application.Dtos;
using CourseRegistration.Application.Interfaces;
using CourseRegistration.Application.Repositories;
using CourseRegistration.Domain.Models;
using CourseRegistration.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseRegistration.Infrastructure.Repositories
{
    public class TeacherSubjectRepository : ITeacherSubjectRepository
    {
        private readonly CourseRegistrationDbcontext _context;
        private readonly ITeacherHttpService _teacherHttpService;

        public DbContext Context => _context;

        public TeacherSubjectRepository(CourseRegistrationDbcontext context, ITeacherHttpService teacherHttpService)
        {
            _context = context;
            _teacherHttpService = teacherHttpService;
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

        async Task<IEnumerable<GetAllTeachersWithSubjectCountResponseDTO>> ITeacherSubjectRepository.GetAllTeachersWithSubjectCount()
        {
            var allTeachers = await _teacherHttpService.GetAllTeachersAsync();

            // Fetch all teacher-subject records once to avoid multiple DB hits in the loop
            var teacherSubjectGroups = await _context.TeacherSubjects
                .Where(ts => ts.IsActive)
                .GroupBy(ts => ts.TeacherId)
                .Select(group => new
                {
                    TeacherId = group.Key,
                    SubjectCount = group.Count()
                })
                .ToListAsync();

            var result = allTeachers.Select(teacher =>
            {
                var subjectCount = teacherSubjectGroups
                    .FirstOrDefault(g => g.TeacherId == teacher.TeacherID)?.SubjectCount ?? 0;

                return new GetAllTeachersWithSubjectCountResponseDTO
                {
                    TeacherId = teacher.TeacherID,
                    TeacherFullName = teacher.TeacherName,
                    EmployeeId = teacher.EmployeeId,
                    TeacherEmail = teacher.Email,
                    SubjectCount = subjectCount
                };
            });

            return result;
        }

    }
}
