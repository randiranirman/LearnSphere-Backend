using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using FileStorage.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Infrastructure.Repositories
{
    public class AssignmentRepository(FileStorageDbContext fileStorageDbContext) : IAssignmentRepository
    {
        public async Task<IEnumerable<GetAllAssignmentBySubjectIdResponse>> GetAllAssignmentDetailsFromSubjectId(int subjectId)
        {
            var result = await fileStorageDbContext.Assignments
                .Where(a => a.SubjectId == subjectId)
                .Select(a => new GetAllAssignmentBySubjectIdResponse
                {
                    Id = a.Id,
                    Title = a.Title,
                    Status = a.Status,
                    NoOfSubmissions = a.Submissions.Count()
                })
                .ToListAsync();

            return result;
        }

        public async Task<int> GetAssignmentCountBySubjectIdAsync(int subjectId)
        {
            var result = await fileStorageDbContext.Assignments
                .Where(a => a.SubjectId == subjectId)
                .CountAsync();

            return result;
        }
    }
}
