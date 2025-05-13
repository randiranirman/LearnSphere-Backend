using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizManagement.Domain.Models;

namespace QuizManagement.Domain.repositories
{

    // subject  service interface 
    public  interface ISubjectService
    {

        Task<List<Subject>> GetAllSubjectsAsync();
        Task<Subject> GetSubjectByIdAsync(int subjectId);
        Task<Subject> CreateSubjectAsync(Subject subject);
        Task<Subject> UpdateSubjectAsync(Subject subject);
        Task<bool> DeleteSubjectAsync(int subjectId);
        Task<List<Subject>> GetSubjectsByTeacherIdAsync(int teacherId);
    }
}
