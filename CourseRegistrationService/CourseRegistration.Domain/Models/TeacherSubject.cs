using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRegistration.Domain.Models
{
    public class TeacherSubject
    {
        public int Id { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [Required]
        public int SubjectId { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string EmployeeId { get; set; } = string.Empty;

        public RegistrationStatus Status { get; set; } = RegistrationStatus.Pending;

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        public DateTime? ApprovedAt { get; set; }

        public int? ApprovedByAdminId { get; set; }

        [MaxLength(500)]
        public string? Remarks { get; set; }
        
        public bool IsActive { get; set; } = true;

        public Subject Subject { get; set; }


    }
}
