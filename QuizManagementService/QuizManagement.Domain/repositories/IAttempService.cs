using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizManagement.Domain.Models;

namespace QuizManagement.Domain.repositories
{
    // quiz attempt service interface
    public interface IAttempService
    {

        Task<int> CreateAttemptAsync(int studentId, int quizId);
        Task<QuizAttempt> GetAttemptByIdAsync(int attemptId);
        Task<List<QuizAttempt>> GetAttemptsByStudentIdAsync(int studentId);
        Task<List<QuizAttempt>> GetAttemptsByQuizIdAsync(int quizId);
        Task<QuestionResponseResult> SaveResponseAsync(int studentId, int quizId, int questionId, string answer, int? optionId);
        Task<int> CompleteAttemptAsync(int studentId, int quizId);
        Task<List<QuizAttemptSummary>> GetQuizResultsAsync(int quizId);
    }
}
