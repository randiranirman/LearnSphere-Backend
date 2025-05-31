using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRegistration.Domain.Models
{
    public  class TeacherClassRegistration
    {
        public int Id { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [Required]
        public int ClassId { get; set; }

        public RegistrationStatus Status { get; set; } = RegistrationStatus.Pending;

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        public DateTime? ApprovedAt { get; set; }

        public int? ApprovedByAdminId { get; set; }

        [MaxLength(500)]
        public string? Remarks { get; set; }

        // Navigation properties
        public virtual Class Class { get; set; } = null!;
    }
}
