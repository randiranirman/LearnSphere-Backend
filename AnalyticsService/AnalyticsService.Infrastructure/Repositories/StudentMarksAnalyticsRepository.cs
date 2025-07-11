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
        public async Task<IEnumerable<AllMarksByStudentIdDTO>> GetAllMarksByStudentIdAsync(int studentId)
        {
            return await analyticsDbContext.StudentMarks
                .Where(sm => sm.StudentId == studentId)
                .Select(sm => new AllMarksByStudentIdDTO
                {
                    AssignmentId = sm.AssignmentId,
                    AssignmentTitle = sm.Assignment.Title,
                    Marks = sm.Marks
                })
                .ToListAsync();
        }
    }
}
