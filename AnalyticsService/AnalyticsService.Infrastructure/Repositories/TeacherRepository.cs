using AnalyticsService.Application.DTOs;
using AnalyticsService.Application.Interfaces;
using AnalyticsService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AnalyticsService.Infrastructure.Repositories
{
    public class TeacherRepository(AnalyticsDbContext analyticsDbContext) : ITeacherRepository
    {
        public async Task<IEnumerable<AllSubjectsByTeacherIdDTO>> GetAllSubjectsByTeacherIdAsync(int teacherId)
        {
            var result = await analyticsDbContext.Subjects
                .Where(s => s.AssignedTeacherId == teacherId)
                .Select(s => new AllSubjectsByTeacherIdDTO
                {
                    SubjectId = s.Id,
                    SubjectTitle = s.Title,
                    SubjectGrade = s.Grade,
                    NoOfAssignments = s.Assignments.Count(),
                    NoOfRegisterdStudents = s.Students.Count()
                })
                .ToListAsync();

            return result;
        }
    }
}
