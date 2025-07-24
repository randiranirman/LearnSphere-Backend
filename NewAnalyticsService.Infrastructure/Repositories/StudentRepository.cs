using Microsoft.EntityFrameworkCore;
using NewAnalyticsService.Application.DTOs;
using NewAnalyticsService.Application.Interfaces;
using NewAnalyticsService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
