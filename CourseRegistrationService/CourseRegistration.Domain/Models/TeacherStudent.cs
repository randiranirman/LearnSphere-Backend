using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRegistration.Domain.Models
{
    public  class TeacherStudent
    {

         public int Id { get; set; }
            
        public int TeacherId { get; set; }
        public  List<int> StudentIds { get; set; } = new List<int>();
        public TeacherStudent(int teacherId, List<int> studentIds)
        {
            TeacherId = teacherId;
            StudentIds = studentIds;
        }


    }
}
