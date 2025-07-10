using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRegistration.Domain.Models
{
    public  class ClassSubject
    {
        [Required]
        public int ClassId { get; set; }

        [Required]
        public int SubjectId { get; set; }

        public DateTime AssociatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Class Class { get; set; } = null!;
        public virtual Subject Subject { get; set; } = null!;
    }
}
