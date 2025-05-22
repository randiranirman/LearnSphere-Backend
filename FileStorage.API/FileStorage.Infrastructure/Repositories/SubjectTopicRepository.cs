using FileStorage.Application.DTOs;
using FileStorage.Domain.Entities;
using FileStorage.Domain.Interfaces;
using FileStorage.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Infrastructure.Repositories
{
    public class SubjectTopicRepository(FileStorageDbContext fileStorageDbContext) : ISubjectTopicRepository
    {
        public async Task<IEnumerable<SubjectEntity>> GetAllSubjectsWithTopicsAsync()
        {
            
        }

        public async Task<IEnumerable<SubjectTopicEntity>> GetTopicsBySubjectIdAsync(int subjectId)
        {
            return await fileStorageDbContext.SubjectTopicEntities
                .Where(x => x.SubjectId == subjectId)
                .ToListAsync();
        }

        public async Task<SubjectTopicEntity?> GetTopicByIdAsync(int topicId)
        {
            return await fileStorageDbContext.SubjectTopicEntities.FirstOrDefaultAsync(x => x.Id == topicId);
        }
    }
}
