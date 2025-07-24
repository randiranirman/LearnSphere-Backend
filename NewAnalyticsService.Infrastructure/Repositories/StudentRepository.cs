using Microsoft.EntityFrameworkCore;
using NewAnalyticsServcie.Application.DTOs;
using NewAnalyticsServcie.Application.Interfaces;
using NewAnalyticsService.Infrastructure.Data;

namespace NewAnalyticsService.Infrastructure.Repositories
{
    public class StudentRepository(NewAnalyticsServiceDbContext newAnalyticsServiceDbContext) : IStudentsRepository
    {
        public async Task<IEnumerable<GetAllStudentMarksByAssignmentIdResponseDTO>> GetAllStudentMarksByAssignmentId(int assignmentId)
        {
            var result = await newAnalyticsServiceDbContext.Marks
                .Where(m => m.AssignmentId == assignmentId)
                .Select(m => new GetAllStudentMarksByAssignmentIdResponseDTO
                {
                    StudentId = m.StudentId,
                    FullName = m.SubmissionName, // Assuming SubmissionName holds student's full name (change if needed)
                    Marks = m.AssignmentMarks
                })
                .ToListAsync();

            return result;
        }
    }
}
