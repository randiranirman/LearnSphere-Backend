using System.ComponentModel.DataAnnotations;

namespace FileStorage.Domain.Entities
{
    public class Material
    {
        [Key]
        public int Id { get; set; }
        public string UploadLink { get; set; }
        public string SavedName { get; set; }
        [Required]
        public int TopicId { get; set; }

        public SubjectTopic SubjectTopic { get; set; }
    }
}
