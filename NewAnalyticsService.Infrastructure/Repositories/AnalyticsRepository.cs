using Microsoft.EntityFrameworkCore;
using NewAnalyticsServcie.Application.DTOs;
using NewAnalyticsServcie.Application.Interfaces;
using NewAnalyticsService.Infrastructure.Data;

namespace NewAnalyticsService.Infrastructure.Repositories
{
    public class AnalyticsRepository(ITeacherSubjectHttpService _teacherSubjectHttpService, IAssignmentHttpService _assignmentHttpService, NewAnalyticsServiceDbContext newAnalyticsServiceDbContext) : IAnalyticsRepository
    {
        public async Task<List<GetAllSubjectsWithAssignmentsAndSubmissionsByTeacherIdResponseDTO>> GetAllSubjectsWithAssignmentsAndSubmissionsByTeacherId(int teacherId)
        {
            var teacherSubjects = await _teacherSubjectHttpService.GetSubjectsByTeacherIdAsync(teacherId);
            var result = new List<GetAllSubjectsWithAssignmentsAndSubmissionsByTeacherIdResponseDTO>();

            foreach (var subject in teacherSubjects)
            {
                var studentCount = await _teacherSubjectHttpService.GetRegisteredStudentCountBySubjectIdAsync(subject.SubjectId);
                var assignmentCount = await _assignmentHttpService.GetAssignmentCountBySubjectIdAsync(subject.SubjectId);

                result.Add(new GetAllSubjectsWithAssignmentsAndSubmissionsByTeacherIdResponseDTO
                {
                    SubjectId = subject.SubjectId,
                    SubjectTitle = subject.Name,
                    SubjectCode = subject.Code,
                    NoOfAssignments = assignmentCount,
                    NoOfRegisterdStudents = studentCount
                });
            }

            return result;
        }

        public async Task<IEnumerable<GetAllAssignmentMarksByStudentIdAndSubjectIdResponseDTO>> GetAllAssignmentMarksByStudentIdAndSubjectId(int studentId, int subjectId)
        {
            var result = await newAnalyticsServiceDbContext.Marks
                .Where(m => m.StudentId == studentId && m.SubjectId == subjectId)
                .Select(m => new GetAllAssignmentMarksByStudentIdAndSubjectIdResponseDTO
                {
                    AssignmentId = m.AssignmentId,
                    AssignmentTitle = m.AssignmentTitle,
                    Marks = m.AssignmentMarks
                })
                .ToListAsync();

            return result;
        }
    }
}
