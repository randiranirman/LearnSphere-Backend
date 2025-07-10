using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Application.Dtos;
using CourseRegistration.Domain.Models;

namespace CourseRegistration.Application.Interfaces
{
    public  interface ISubjectService
    {
        Task<Subject> CreatedSubjectAsync(CreateSubjectRequest request);
        Task<IEnumerable<Subject>> GetAllSubjectsAsync();
        Task<Subject?> GetSubjectByIdAsync(int id);
        Task<Subject?> GetSubjectByCodeAsync(string code);
        Task<IEnumerable<Subject>> GetSubjectsByTeacherIdAsync(int teacherId);
        Task<IEnumerable<Subject>> GetSubjectsByStudentIdAsync(int studentId);
        Task<IEnumerable<Subject>> GetSubjectsByGradeIdAsync(int gradeId);
        Task<bool> DeleteSubjectAsync(int id);
        Task<Subject?> UpdateSubjectAsync(int id, CreateSubjectRequest request);


    }
}
