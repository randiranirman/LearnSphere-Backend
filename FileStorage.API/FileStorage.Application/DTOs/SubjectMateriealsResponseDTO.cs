using FileStorage.Domain.Entities;

namespace FileStorage.Application.DTOs
{
    public class SubjectMateriealsResponseDTO
    {
        public int SubjectTopicId { get; set; }
        public string SubjectTopicName { get; set; }
        public ICollection<Material> Materials { get; set; }
    }
}
