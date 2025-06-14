using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRegistration.Application.Repositories
{
    public  interface IUnitOfWork : IDisposable
    {
        ISubjectRepository Subjects { get; }
        IClassRepository Classes { get; }
        ITeacherSubjectRepository TeacherSubjects { get; }
        IStudentSubjectRepository StudentSubjects { get; }
        IStudentClassRegistrationRepository StudentClassRegistrations { get; }
        ITeacherClassRegistrationRepository TeacherClassRegistrations { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
