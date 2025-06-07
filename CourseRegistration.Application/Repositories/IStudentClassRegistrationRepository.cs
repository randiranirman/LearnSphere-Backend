using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Domain.Models;

namespace CourseRegistration.Application.Repositories
{
    public interface IStudentClassRegistrationRepository  : IRepository<StudentClassRegistration>
    {

        Task<IEnumerable<StudentClassRegistration>> GetByStudentIdAsync(int studentId);
        Task<IEnumerable<StudentClassRegistration>> GetByClassIdAsync(int classId);
        Task<IEnumerable<StudentClassRegistration>> GetByStatusAsync(RegistrationStatus status);
        Task<StudentClassRegistration?> GetByStudentAndClassAsync(int studentId, int classId);
        Task<IEnumerable<StudentClassRegistration>> GetPendingRegistrationsAsync();
        Task<int> GetRegisteredStudentCountAsync(int classId);


    }
}
