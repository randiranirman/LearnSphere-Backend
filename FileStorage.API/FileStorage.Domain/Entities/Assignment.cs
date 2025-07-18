using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileStorage.Domain.Entities
{
    public class Assignment
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public DateTime DueTime { get; set; }

        public string UploadLink { get; set; }

        [NotMapped]
        public string Status => DateTime.UtcNow > DueTime ? "complete" : "incomplete";

        [Required]
        public int ClassId { get; set; }

        [Required]
        public int SubjectId { get; set; }

        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();

    }
}
