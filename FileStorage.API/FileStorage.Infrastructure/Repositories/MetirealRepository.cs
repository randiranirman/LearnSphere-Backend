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

        public async Task<MetirealDTO?> DeleteMetireal(int metirealId)
        {
            var existingMetireal = await fileStorageDbContext.MetirialEntities.FirstOrDefaultAsync(a => a.Id == metirealId);
            if (existingMetireal == null) return null;
            else
            {
                fileStorageDbContext.MetirialEntities.Remove(existingMetireal);
                await fileStorageDbContext.SaveChangesAsync();
            }

            var returnModel = new MetirealDTO
            {
                Id = existingMetireal.Id,
                UploadLink = existingMetireal.UploadLink,
                FileType = existingMetireal.FileType,
                SavedName = existingMetireal.SavedName
            };

            return returnModel;

        }

        public async Task<MetirealDTO?> UpdateMetirealSavedName(int metirealId, UpdateMetirealSavedNameDTO newSavedName)
        {
            var existingMetireal = await fileStorageDbContext.MetirialEntities.FirstOrDefaultAsync(x => x.Id == metirealId);
            if (existingMetireal == null) return null;

            existingMetireal.SavedName = newSavedName.UpdatedSavedName;

            await fileStorageDbContext.SaveChangesAsync();

            var returnModel = new MetirealDTO
            {
                Id = existingMetireal.Id,
                UploadLink = existingMetireal.UploadLink,
                FileType = existingMetireal.FileType,
                SavedName = existingMetireal.SavedName
            };
            return returnModel;
        }
    }
}
