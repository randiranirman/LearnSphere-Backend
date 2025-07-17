using AnalyticsService.Application.DTOs;
using AnalyticsService.Application.Interfaces;
using AnalyticsService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AnalyticsService.Infrastructure.Repositories
{
    public class AnalyticsRepository(AnalyticsDbContext analyticsDbContext) : IAnalyticsRepository
    {
        public async Task<GetOverviewDTO> GetNoOfStudentsAndNoOfTeacher()
        {
            var studentStats = await analyticsDbContext.Students
                .GroupBy(s => 1)
                .Select(g => new
                {
                    NoOfStudents = g.Count(),
                    MinGrade = g.Min(s => s.Grade),
                    MaxGrade = g.Max(s => s.Grade)
                })
            .FirstOrDefaultAsync();

            var noOfTeachers = await analyticsDbContext.Teachers.CountAsync();

            return new GetOverviewDTO
            {
                NoOfStudents = studentStats?.NoOfStudents ?? 0,
                MinGrade = studentStats?.MinGrade ?? 0,
                MaxGrade = studentStats?.MaxGrade ?? 0,
                NoOfTeachers = noOfTeachers
            };
        }


        public async Task<IEnumerable<TeachersWithSubjectsCountDTO>> GetTeachersWithSubjectsCounts()
        {
            var teachersWithCounts = await analyticsDbContext.Teachers
                .Select(t => new TeachersWithSubjectsCountDTO
                {
                    TeacherId = t.Id,
                    EmployeeId = t.EmployeeId,
                    TeacherFullName = t.FullName,
                    TeacherEmail = t.Email,
                    SubjectCount = t.AssignedSubjects.Count
                })
                .ToListAsync();

            return teachersWithCounts;
        }
        public async Task<IEnumerable<SubejctsWithRegisteredStudentsCountDTO>> GetTeacherSubejctsWithStudentCount(int teacherId)
        {
            var response = await analyticsDbContext.Subjects
                .Where(s => s.AssignedTeacherId == teacherId)
                .Select(s => new SubejctsWithRegisteredStudentsCountDTO
                {
                    SubjectId = s.Id,
                    SubjectTitle = s.Title,
                    Grade = s.Grade,
                    RegisteredStudentCount = s.Students.Count
                })
                .ToListAsync();

            return response;
        }

        public async Task<IEnumerable<StudentDetailsResponseDTO>> GetAllStudentsRegistered()
        {
            var response = await analyticsDbContext.Students
                .Select(s => new StudentDetailsResponseDTO
                {
                    Id = s.Id,
                    IndexNo = s.IndexNumber,
                    FullName = s.FullName,
                    Grade = s.Grade
                })
                .ToListAsync();
            return response;
        }

        public async Task<StudentDetailsResponseDTO> GetStudentByIndexNo(string IndexNo)
        {
            string normalized = IndexNo.ToUpper();
            var response = await analyticsDbContext.Students
                .Where(s => s.IndexNumber.ToUpper() == normalized)
                .Select(s => new StudentDetailsResponseDTO
                {
                    Id = s.Id,
                    IndexNo = s.IndexNumber,
                    FullName = s.FullName,
                    Grade = s.Grade
                })
                .FirstOrDefaultAsync();
            return response;
        }
    }
}
