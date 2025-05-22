using FileStorage.Domain.Entities;

namespace FileStorage.Domain.Interfaces
{
    public interface ISubjectTopicRepository
    {
        public Task<IEnumerable<SubjectEntity>> GetAllSubjectsWithTopicsAsync();
        public Task<IEnumerable<SubjectTopicEntity>> GetTopicsBySubjectIdAsync(int subjectId);
        public Task<SubjectTopicEntity?> GetTopicByIdAsync(int topicId);
    }
}
