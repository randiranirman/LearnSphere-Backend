using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnalyticsService.Domain.Entities
{
    public class Submission
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Assignment")]
        public int AssignmentId { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public DateTime AssignmentDueTime { get; set; }

        [NotMapped]
        public string Status => AssignmentDueTime >= SubmitedTime ? "Not overdue" : "Overdue";

        public string UploadLink { get; set; } // this stores the link to the file storage which store the submission file
        public string SubmissionName { get; set; }
        public DateTime SubmitedTime { get; set; }

        public Assignment Assignment { get; set; }
        public Student Student { get; set; }

    }
}
