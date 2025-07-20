using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRegistration.Domain.Models
{
    public class StudentClassRegistration
    {
        public int StudentRegistrationId { get; set; }

        [Required]
        public int StudentId { get; set; }
        

        [Required]
        public int ClassId { get; set; }


        [Required]
        [MaxLength(20)]
        public string IndexNumber { get; set; } = string.Empty;

        public RegistrationStatus Status { get; set; } = RegistrationStatus.Pending;

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        public DateTime? ApprovedAt { get; set; }

        public int? ApprovedByAdminId { get; set; }

        [MaxLength(500)]
        public string? Remarks { get; set; }

        // Navigation properties
        public virtual Class Class { get; set; } = null!;
        public virtual ICollection<StudentRegistrationSubject> RegistrationSubjects { get; set; } = new List<StudentRegistrationSubject>();
    }
}
