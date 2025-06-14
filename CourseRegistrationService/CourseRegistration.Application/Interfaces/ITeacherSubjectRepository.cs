using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Domain.Models;

namespace CourseRegistration.Application.Repositories
{
    public  interface ITeacherSubjectRepository : IRepository<TeacherSubject>
    {
        Task<IEnumerable<TeacherSubject>> GetByTeacherIdAsync(int teacherId);
        Task<IEnumerable<TeacherSubject>> GetBySubjectIdAsync(int subjectId);
        Task<TeacherSubject?> GetByTeacherAndSubjectAsync(int teacherId, int subjectId);
        Task<bool> IsTeacherAssignedToSubjectAsync(int teacherId, int subjectId);
    }
}
