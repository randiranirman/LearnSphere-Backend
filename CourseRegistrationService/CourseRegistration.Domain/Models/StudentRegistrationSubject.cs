using System.ComponentModel.DataAnnotations;

namespace CourseRegistration.Domain.Models
{
    public class StudentRegistrationSubject
    {
        public int Id { get; set; }

        [Required]
        public int StudentRegistrationId { get; set; }

        [Required]
        public int SubjectId { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual StudentClassRegistration StudentRegistration { get; set; } = null!;
        public virtual Subject Subject { get; set; } = null!;
    }
}
