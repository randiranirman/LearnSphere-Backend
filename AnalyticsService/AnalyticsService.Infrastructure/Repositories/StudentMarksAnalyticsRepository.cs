using AnalyticsService.Application.DTOs;
using AnalyticsService.Application.Interfaces;
using AnalyticsService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AnalyticsService.Infrastructure.Repositories
{
    public class StudentMarksAnalyticsRepository(AnalyticsDbContext analyticsDbContext) : IStudentMarksAnalyticsRepository
    {

        public async Task<IEnumerable<StudentDTO>> GetAllStudentsAsync(int subjectId)
        {
            var studentDomainModel = await analyticsDbContext.Subjects
                .Where(s => s.Id == subjectId)
                .SelectMany(s => s.Students)
                .Select(s => new StudentDTO
                {
                    Id = s.Id,
                    FullName = $"{s.FirstName} {s.LastName}",
                    IndexNumber = s.IndexNumber
                })
                .ToListAsync();
            return studentDomainModel;
        }
        public async Task<IEnumerable<AllMarksByStudentIdDTO>> GetAllMarksByStudentIdAsync(int subjectId, int studentId)
        {
            var result = await (from sm in analyticsDbContext.StudentMarks
                                join a in analyticsDbContext.Assignments
                                    on sm.AssignmentId equals a.Id
                                where sm.StudentId == studentId && a.SubjectId == subjectId
                                select new AllMarksByStudentIdDTO
                                {
                                    AssignmentId = a.Id,
                                    AssignmentTitle = a.Title,
                                    Marks = sm.Marks
                                }).ToListAsync();

            return result;

        }
    }
}
