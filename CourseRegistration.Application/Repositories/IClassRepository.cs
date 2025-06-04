using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Domain.Models;

namespace CourseRegistration.Application.Repositories
{
    public  interface IClassRepository: IRepository<Class>
    {

        Task<IEnumerable<Class>> GetClassesBySubjectIdAsync(int subjectId);
        Task<IEnumerable<Class>> GetClassesByGradeAsync(int grade);
        Task<IEnumerable<Class>> GetClassesByStatusAsync(ClassStatus status);
        Task<Class?> GetClassWithRegistrationsAsync(int classId);
        Task<IEnumerable<Class>> GetClassesByTeacherIdAsync(int teacherId);
        Task<IEnumerable<Class>> GetClassesByStudentIdAsync(int studentId);
    }
}
