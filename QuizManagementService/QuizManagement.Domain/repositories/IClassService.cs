using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizManagement.Domain.Models;

namespace QuizManagement.Domain.repositories
{

    // class service interface 
    public  interface IClassService
    {

        Task<List<Class>> GetAllClassesAsync();
        Task<Class> GetClassByIdAsync(int classId);
        Task<Class> CreateClassAsync(Class classEntity);
        Task<Class> UpdateClassAsync(Class classEntity);
        Task<bool> DeleteClassAsync(int classId);
        Task<List<Class>> GetClassesByTeacherIdAsync(int teacherId);
        Task<bool> AddStudentToClassAsync(int classId, int studentId);
        Task<bool> RemoveStudentFromClassAsync(int classId, int studentId);


    }
}
