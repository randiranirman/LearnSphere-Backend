using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnalyticsService.Domain.Entities
{
    public class SubjectTopic
    {
        [Key]
        public int Id { get; set; }
        public string? TopicName { get; set; }

        [ForeignKey("SubjectEntity")]
        public int SubjectId { get; set; }

        public Subject SubjectEntity { get; set; }

        public ICollection<Metirial> MetirialEntities { get; set; }
    }
}
