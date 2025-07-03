using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using FileStorage.Domain.Entities;
using FileStorage.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Infrastructure.Repositories
{
    public class SubjectTopicRepository(FileStorageDbContext fileStorageDbContext) : ISubjectTopicRepository
    {

        public async Task<IEnumerable<SubjectTopicEntity>> GetTopicsBySubjectIdAsync(int subjectId)
        {
            return await fileStorageDbContext.SubjectTopicEntities
                .Where(x => x.SubjectId == subjectId)
                .ToListAsync();
        }

        public async Task<SubjectTopicEntity> GetTopicByIdAsync(int topicId)
        {
            return await fileStorageDbContext.SubjectTopicEntities.FirstOrDefaultAsync(x => x.Id == topicId);
        }

        public async Task<IEnumerable<SubjectDTO>> GetAllSubjectsWithTopicsAsync()
        {
            var subjects = await fileStorageDbContext.SubjectEntities
                                .Include(a => a.SubjectTopicEntities)
                                .Select(subject => new SubjectDTO
                                {
                                    Id = subject.Id,
                                    Code = subject.Code,
                                    CreatedDate = subject.CreatedDate,
                                    Topics = subject.SubjectTopicEntities.Select(topic => new TopicDTO
                                    {
                                        Id = topic.Id,
                                        TopicName = topic.TopicName
                                    }).ToList()
                                }).ToListAsync();
            return subjects;
        }
    }
}
