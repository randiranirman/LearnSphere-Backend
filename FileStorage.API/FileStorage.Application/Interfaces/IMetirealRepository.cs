using FileStorage.Application.DTOs;
using FileStorage.Domain.Entities;

namespace FileStorage.Application.Interfaces
{
    public interface IMetirealRepository
    {
        public Task<IEnumerable<GetAllTopicsWithMetireals>> GetAllMetirealsWithTopicsAsync();
        public Task<IEnumerable<MetirialEntity>> GetAllMetirealsByTopicId(int topicId);
        public Task<CreateMetirealResponseDTO> CreateMetireal(int topicId, CreateMetirealRequestDTO metirialEntity);
    }
}
