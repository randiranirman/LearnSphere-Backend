using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRegistration.Domain.Models
{
    public  class Class
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int SubjectId { get; set; }

        [Required]
        public int Grade { get; set; } // Grade level (1-12)

        [MaxLength(500)]
        public string? Description { get; set; }

        public int MaxStudents { get; set; } = 30;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ClassStatus Status { get; set; } = ClassStatus.Draft;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Subject Subject { get; set; } = null!;
        public virtual ICollection<StudentClassRegistration> StudentRegistrations { get; set; } = new List<StudentClassRegistration>();
        public virtual ICollection<TeacherClassRegistration> TeacherRegistrations { get; set; } = new List<TeacherClassRegistration>();
    }
}
