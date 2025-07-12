using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnalyticsService.Domain.Entities
{
    public class Assignment
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public DateTime DueTime { get; set; }

        [NotMapped]
        public string Status => DateTime.UtcNow > DueTime ? "complete" : "incomplete";

        public int Grade { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public ICollection<Submission> Submissions { get; set; }

    }
}
