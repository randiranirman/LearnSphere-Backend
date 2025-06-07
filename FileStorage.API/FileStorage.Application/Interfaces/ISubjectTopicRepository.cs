using FileStorage.Application.DTOs;
using FileStorage.Domain.Entities;

namespace FileStorage.Application.Interfaces
{
    public interface ISubjectTopicRepository
    {
        public Task<IEnumerable<SubjectDTO>> GetAllSubjectsWithTopicsAsync();
        public Task<IEnumerable<SubjectTopicEntity>> GetTopicsBySubjectIdAsync(int subjectId);
        public Task<SubjectTopicEntity> GetTopicByIdAsync(int topicId);
        public Task<TopicDTO?> EditTopicNameAsync(int topicId, string newTopicName);
    }
}
