using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManagement.Domain.Models
{
    public  class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }

        public TeacherDto Teacher { get; set; }
        public List<StudentDto> Students { get; set; }
        public string Description { get; set; }
        public List<Quiz> Quizzes { get; set; }
    }
}
