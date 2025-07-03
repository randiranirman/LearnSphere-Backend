using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Application.DTOs
{
    public class GetAllMetirealResponseDTO
    {
        public int Id { get; set; }
        public string? UploadLink { get; set; }
        public string? FileType { get; set; }
        public string? SavedName { get; set; }
        public int TopicId { get; set; }
    }
}
