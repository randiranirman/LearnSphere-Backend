using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace QuizManagement.Domain.Models
{
    public  class Question
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; } // Multiple Choice, True/False, Short Answer, etc.
        public int Marks { get; set; }
        public List<Option> Options { get; set; } // For multiple choice questions
        public string CorrectAnswer { get; set; } // For text/short answer questions
    }
}
