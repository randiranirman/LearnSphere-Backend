using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Domain.Models;

namespace CourseRegistration.Application.Repositories
{
    public  interface ITeacherClassRegistrationRepository : IRepository<TeacherClassRegistration>
    {
        Task<IEnumerable<TeacherClassRegistration>> GetByTeacherIdAsync(int teacherId);
        Task<IEnumerable<TeacherClassRegistration>> GetByClassIdAsync(int classId);
        Task<IEnumerable<TeacherClassRegistration>> GetByStatusAsync(RegistrationStatus status);
        Task<TeacherClassRegistration?> GetByTeacherAndClassAsync(int teacherId, int classId);
        Task<IEnumerable<TeacherClassRegistration>> GetPendingRegistrationsAsync();
    }

}

