using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRegistration.Domain.Models
{
    public  class Subject 
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
      
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // Navigation properties
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();
        public virtual ICollection<TeacherClassRegistration> TeacherClassRegistrations { get; set; } = new List<TeacherClassRegistration>();
        public ICollection<Class> Classes { get; set; } = new List<Class>();


    }
}
