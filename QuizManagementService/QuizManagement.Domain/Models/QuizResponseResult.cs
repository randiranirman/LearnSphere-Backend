using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManagement.Domain.Models
{
    public  class QuizResponseResult
    {

        public bool IsCorrect { get; set; }
        public int Score { get; set; }
    }
}
