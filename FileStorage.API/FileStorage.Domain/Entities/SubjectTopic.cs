using System.ComponentModel.DataAnnotations;

namespace FileStorage.Domain.Entities
{
    public class SubjectTopic
    {
        public int Id { get; set; }
        public string TopicName { get; set; }

        [Required]
        public int SubjectId { get; set; }

        public ICollection<Material?> Materials { get; set; }
    }
}
