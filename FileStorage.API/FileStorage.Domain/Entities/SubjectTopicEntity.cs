using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileStorage.Domain.Entities
{
    public class SubjectTopicEntity
    {
        [Key]
        public int Id { get; set; }
        public string? TopicName { get; set; }

        [ForeignKey("SubjectEntity")]
        public int SubjectId { get; set; }
        
        public SubjectEntity SubjectEntity { get; set; }

        public ICollection<MetirialEntity> MetirialEntities { get; set; }
    }
}
