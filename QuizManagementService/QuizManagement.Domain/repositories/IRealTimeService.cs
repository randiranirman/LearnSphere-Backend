using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizManagement.Domain.Models;

namespace QuizManagement.Domain.repositories
{
    public  interface IRealTimeService
    {

        Task NotifyQuizStartedAsync(int quizId, int classId);
        Task NotifyStudentJoinedAsync(int studentId, int quizId);
        Task NotifyStudentAnsweredAsync(int studentId, int questionId, bool isCorrect, int score);
        Task NotifyStudentCompletedAsync(int studentId, int finalScore);
        Task NotifyQuizEndedAsync(int quizId);
        Task SendQuestionToStudentAsync(int studentId, Question question);
        Task SendAnswerResultToStudentAsync(int studentId, bool isCorrect, int score);
        Task SendQuizCompletedToStudentAsync(int studentId, int finalScore);
    }
}
