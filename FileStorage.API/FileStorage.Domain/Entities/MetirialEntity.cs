using System.ComponentModel.DataAnnotations.Schema;

namespace FileStorage.Domain.Entities
{
    public class MetirialEntity
    {
        public int Id { get; set; }
        public string UploadLink { get; set; }
        public string FileType { get; set; }
        public string SavedName { get; set; }

        public int TopicId { get; set; }
        public SubjectTopicEntity SubjectTopic { get; set; }
    }
}
