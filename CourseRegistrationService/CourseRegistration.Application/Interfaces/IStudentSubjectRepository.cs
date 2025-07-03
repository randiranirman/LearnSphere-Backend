using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Domain.Models;

namespace CourseRegistration.Application.Repositories
{
    public interface IStudentSubjectRepository : IRepository<StudentSubject>
    {

        Task<IEnumerable<StudentSubject>> GetByStudentIdAsync(int studentId);
        Task<IEnumerable<StudentSubject>> GetBySubjectIdAsync(int subjectId);
        Task<StudentSubject?> GetByStudentAndSubjectAsync(int studentId, int subjectId);
        Task<bool> IsStudentEnrolledInSubjectAsync(int studentId, int subjectId);
    }
}
