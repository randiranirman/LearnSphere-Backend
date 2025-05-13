
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManagement.Domain.Models
{
    public  class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; } // Foreign key
        public TeacherDto Teacher { get; set; } // Non-tracked DTO
        public List<StudentDto> Students { get; set; } // List of DTOs, not tracked entities
    }
}
