using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace QuizManagement.Domain.Models
{
    public  class Quiz
    {
        public int  Id { get; set; }
        public string Title { get; set; }
        public string  Description  { get; set; }
        public int SubjectID { get; set; }

        public Subject Subject { get; set; }
        public int TeacherID;
        public Teacher Teacher { get; set; }
        public int ClassID;

        public Class Class { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime  EndTime { get; set; }
        public int TotalMarks { get; set; }
        public bool  IsActive { get; set; }
        public List<Question> Questions { get; set; }




    }
}
