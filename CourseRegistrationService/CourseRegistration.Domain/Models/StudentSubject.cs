using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRegistration.Domain.Models
{
    public class StudentSubject
    {
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }//  this is coming   from student  model in user management

        [Required]
        public int SubjectId { get; set; }

        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public  Subject Subject { get; set; } = null!;
    }
}
