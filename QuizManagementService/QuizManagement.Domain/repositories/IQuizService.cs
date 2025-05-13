using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizManagement.Domain.Models;

namespace QuizManagement.Domain.repositories
{

    // quiz service interface
    public  interface IQuizService
    {

        Task<List<Quiz>> GetAllQuizzesAsync();
        Task<Quiz> GetQuizByIdAsync(int quizId);
        Task<List<Quiz>> GetQuizzesByTeacherIdAsync(int teacherId);
        Task<List<Quiz>> GetQuizzesByClassIdAsync(int classId);
        Task<List<Quiz>> GetQuizzesBySubjectIdAsync(int subjectId);
        Task<Quiz> CreateQuizAsync(Quiz quiz);
        Task<Quiz> UpdateQuizAsync(Quiz quiz);
        Task<bool> DeleteQuizAsync(int quizId);
        Task<bool> ActivateQuizAsync(int quizId);
        Task<bool> DeactivateQuizAsync(int quizId);
        Task<Question> GetNextQuestionAsync(int quizId, int studentId);
        Task<bool> HasMoreQuestionsAsync(int quizId, int studentId);
    }
}
