using System.ComponentModel.DataAnnotations;

namespace FileStorage.Application.DTOs
{
    public class CreateMaterialResponseDTO
    {
        public int MaterialId { get; set; }
        public string UploadLink { get; set; }
        public string SavedName { get; set; }
        public int TopicId { get; set; }
    }
}
