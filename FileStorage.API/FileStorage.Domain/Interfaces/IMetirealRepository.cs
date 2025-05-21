using FileStorage.Domain.Entities;

namespace FileStorage.Domain.Interfaces
{
    public interface IMetirealRepository
    {
        public Task<IEnumerable<MetirialEntity>> GetAllMetirealsByTopicId(int topicId);
        public Task<MetirialEntity> CreateMetireal(int topicId, MetirialEntity metirialEntity);
    }
}
