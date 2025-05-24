using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using FileStorage.Domain.Entities;
using FileStorage.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Infrastructure.Repositories
{
    public class MetirealRepository(FileStorageDbContext fileStorageDbContext) : IMetirealRepository
    {

        public async Task<IEnumerable<GetAllTopicsWithMetireals>> GetAllMetirealsWithTopicsAsync()
        {
            var responseType = await fileStorageDbContext.SubjectTopicEntities
                                    .Include(a => a.MetirialEntities)
                                    .Select(topic => new GetAllTopicsWithMetireals
                                    {
                                        Id = topic.Id,
                                        Name = topic.TopicName,
                                        Items = topic.MetirialEntities.Select(metireal => new MetirealDTO
                                        {
                                            Id = metireal.Id,
                                            UploadLink = metireal.UploadLink,
                                            FileType = metireal.FileType,
                                            SavedName = metireal.SavedName
                                        }).ToList()
                                    }).ToListAsync();
            return responseType;
        }

        public async Task<IEnumerable<MetirialEntity>> GetAllMetirealsByTopicId(int topicId)
        {
            return await fileStorageDbContext.MetirialEntities
                .Where(x => x.TopicId == topicId)
                .ToListAsync();
        }
        public async Task<CreateMetirealResponseDTO> CreateMetireal(int topicId, CreateMetirealRequestDTO metirialEntity)
        {
            var metirealDomainModel = new MetirialEntity
            {
                UploadLink = metirialEntity.UploadLink,
                FileType = metirialEntity.FileType,
                SavedName = metirialEntity.SavedName,
                TopicId = topicId
            };
            
            fileStorageDbContext.MetirialEntities.Add(metirealDomainModel);
            await fileStorageDbContext.SaveChangesAsync();

            var responseModel = new CreateMetirealResponseDTO
            {
                TopicId = topicId,
                Metireal = new MetirealDTO
                {
                    Id = metirealDomainModel.Id,
                    UploadLink = metirealDomainModel.UploadLink,
                    FileType = metirealDomainModel.FileType,
                    SavedName = metirealDomainModel.SavedName
                }
            };

            return responseModel;
        }
    }
}
