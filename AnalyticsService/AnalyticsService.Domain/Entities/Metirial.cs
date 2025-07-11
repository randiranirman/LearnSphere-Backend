using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AnalyticsService.Domain.Entities
{
    public class Metirial
    {
        [Key]
        public int Id { get; set; }
        public string UploadLink { get; set; }
        public string FileType { get; set; }
        public string SavedName { get; set; }

        [ForeignKey("SubjectTopic")]
        public int TopicId { get; set; }

        public SubjectTopic SubjectTopicEntity { get; set; }
    }
}
