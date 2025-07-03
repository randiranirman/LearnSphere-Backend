using CourseManagementService.Data;
using CourseManagementService.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementService.Repositories
{
    public class SQLTeacherSubjectRepository : ITeacherSubjectRepository
    {
        private readonly LmsContext _context;
        public SQLTeacherSubjectRepository(LmsContext context)
        {
            _context = context;
        }
        public async Task<List<TeacherSubject>> GetTeacherSubjectAsync()
        {
            return await _context.TeacherSubjects.Include("SubjectCodeNavigation").Include("Teacher").ToListAsync();
        }

        public async Task<TeacherSubject?> GetTeacherSubjectById(Guid subjectCode)
        {
            var existingTeacherSubject = await _context.TeacherSubjects.Include("SubjectCodeNavigation").Include("Teacher").FirstOrDefaultAsync(teacherSubject => teacherSubject.SubjectCode == subjectCode);

            if (existingTeacherSubject is null)
                return null;

            return existingTeacherSubject;
        }
    }
}
