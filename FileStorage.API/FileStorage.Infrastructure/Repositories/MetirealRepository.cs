using FileStorage.Application.Interfaces;
using FileStorage.Domain.Entities;
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
        public async Task<MetirialEntity> CreateMetireal(int topicId, MetirialEntity metirialEntity)
        {
            
            fileStorageDbContext.MetirialEntities.Add(metirialEntity);
            await fileStorageDbContext.SaveChangesAsync();

            metirialEntity.SubjectTopicEntity = await fileStorageDbContext.SubjectTopicEntities.FirstOrDefaultAsync(x => x.Id == topicId);
            return metirialEntity;
        }
    }
}
