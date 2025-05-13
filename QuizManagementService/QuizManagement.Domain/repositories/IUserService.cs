using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizManagement.Domain.Models;

namespace QuizManagement.Domain.repositories
{
    public  interface IUserService
    {
        Task<TeacherDto> GetTeacherAsync(int teacherId);
        Task<StudentDto> GetStudentAsync(int studentId);
        Task<List<StudentDto>> GetStudentsByClassAsync(int classId);
        Task<bool> ValidateTeacherForSubjectAsync(int teacherId, int subjectId);
        Task<bool> ValidateTeacherForClassAsync(int teacherId, int classId);
        Task<bool> ValidateStudentForClassAsync(int studentId, int classId);
    }
}
