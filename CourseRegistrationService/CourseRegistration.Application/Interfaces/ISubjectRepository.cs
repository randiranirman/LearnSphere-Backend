using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Domain.Models;

namespace CourseRegistration.Application.Repositories
{
    public  interface ISubjectRepository: IRepository<Subject>
    {

        Task<Subject?> GetByCodeAsync(string code);
        Task<IEnumerable<Subject>> GetSubjectsByTeacherIdAsync(int teacherId);
        Task<IEnumerable<Subject>> GetSubjectByStudentIdAsync(int studentId);
        Task<IEnumerable<Subject>> GetSubjectsByGradeIdAsync(int grade);

        
    }
}
