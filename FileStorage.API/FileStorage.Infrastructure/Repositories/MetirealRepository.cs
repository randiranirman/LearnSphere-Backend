using FileStorage.Domain.Entities;
using FileStorage.Domain.Interfaces;
using FileStorage.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Infrastructure.Repositories
{
    public class MetirealRepository(FileStorageDbContext fileStorageDbContext) : IMetirealRepository
    {
        public async Task<IEnumerable<MetirialEntity>> GetAllMetirealsByTopicId(int topicId)
        {
            return await fileStorageDbContext.MetirialEntities
                .Where(x => x.TopicId == topicId)
                .ToListAsync();
        }
    }
}
