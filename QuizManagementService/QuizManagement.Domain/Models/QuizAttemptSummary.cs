using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManagement.Domain.Models
{
    public  class QuizAttemptSummary
    {

        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int TotalScore { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public DateTime CompletionTime { get; set; }
    }
}
