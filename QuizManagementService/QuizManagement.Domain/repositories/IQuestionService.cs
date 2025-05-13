using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizManagement.Domain.Models;

namespace QuizManagement.Domain.repositories
{
    // question service interaface
    public interface IQuestionService
    {

        Task<Question> GetQuestionByIdAsync(int questionId);
        Task<List<Question>> GetQuestionsByQuizIdAsync(int quizId);
        Task<Question> CreateQuestionAsync(Question question);
        Task<Question> UpdateQuestionAsync(Question question);
        Task<bool> DeleteQuestionAsync(int questionId);
        Task<Option> CreateOptionAsync(Option option);
        Task<bool> DeleteOptionAsync(int optionId);
        Task<bool> ValidateAnswerAsync(int questionId, string answer, int? optionId);
        Task<int> ScoreAnswerAsync(int questionId, string answer, int? optionId);
    }
}
