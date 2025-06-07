using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileStorage.Domain.Entities
{
    public class MetirialEntity
    {
        [Key]
        public int Id { get; set; }
        public string UploadLink { get; set; }
        public string FileType { get; set; }
        public string SavedName { get; set; }

        [ForeignKey("SubjectTopicEntity")]
        public int TopicId { get; set; }

        public SubjectTopicEntity SubjectTopicEntity { get; set; }
    }
}
