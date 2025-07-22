using AnalyticsService.Application.DTOs;
using AnalyticsService.Application.Interfaces;
using AnalyticsService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AnalyticsService.Infrastructure.Repositories
{
    public class AssignmentsRepository(AnalyticsDbContext analyticsDbContext) : IAssignmentsRepository
    {
        public async Task<IEnumerable<AssignmentDTO>> GetAllAssignmentsBySubjectIdAsync(int subjectId)
        {
            var result = await analyticsDbContext.Assignments
                .Where(a => a.SubjectId == subjectId)
                .Select(a => new AssignmentDTO
                {
                    Id = a.Id,
                    Title = a.Title,
                    Status = a.Status,
                    NoOfSubmissions = a.Submissions.Count()
                })
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<StudentsByAssignmentIdResponseDTO>> GetAllStudentsByAssignmentIdAsync(int assignmentId)
        {
            var result = await analyticsDbContext.StudentMarks
                .Where(sm => sm.AssignmentId == assignmentId)
                .Select(sm => new StudentsByAssignmentIdResponseDTO
                {
                    StudentId = sm.Student.Id,
                    FullName = sm.Student.FullName,
                    Marks = sm.Marks
                })
                .ToListAsync();

            return result;
        }
    }
}
