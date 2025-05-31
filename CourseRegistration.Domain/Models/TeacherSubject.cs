using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRegistration.Domain.Models
{
    public  class TeacherSubject
    {
        [Required]
        public int Id { get; set; }


        public int SubjectID { get; set; }
        [Required]
        public int TeacherID { get; set; }
        public  DateTime AssignedAt { get; set; } = DateTime.Now;
        public bool  IsActive { get; set; } = true;
        // navigation pros 
        public virtual Subject? Subject { get; set; }
    }
}
