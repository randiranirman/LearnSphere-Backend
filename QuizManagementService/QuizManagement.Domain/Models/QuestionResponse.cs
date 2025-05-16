using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManagement.Domain.Models
{
    public  class QuestionResponse
    {

        public int Id { get; set; }
        public int QuizAttemptId { get; set; }
        public QuizAttempt QuizAttempt { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public string StudentAnswer { get; set; }
        public int? SelectedOptionId { get; set; } // For multiple choice questions
        public Option SelectedOption { get; set; }
        public int Score { get; set; }
        public bool IsCorrect { get; set; }
        public DateTime AnsweredAt { get; set; }
    }
}
