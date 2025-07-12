using CourseRegistration.Domain.Models;

namespace CourseRegistration.Application.Repositories
{
    public interface IStudentRegistrationSubjectRepository : IRepository<StudentRegistrationSubject>
    {
        Task<IEnumerable<StudentRegistrationSubject>> GetByRegistrationIdAsync(int registrationId);
        Task<IEnumerable<StudentRegistrationSubject>> GetBySubjectIdAsync(int subjectId);
        Task<StudentRegistrationSubject?> GetByRegistrationAndSubjectAsync(int registrationId, int subjectId);
        Task DeleteByRegistrationIdAsync(int registrationId);
    }
}
