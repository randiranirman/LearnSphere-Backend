using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManagement.Domain.Models
{
    public  class QuizAttempt
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int TotalScore { get; set; }
        public bool IsCompleted { get; set; }
        public List<QuestionResponse> Responses { get; set; }
    }
}
