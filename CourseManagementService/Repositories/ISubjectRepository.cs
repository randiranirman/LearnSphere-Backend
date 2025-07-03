using CourseManagementService.Models.Domains;

namespace CourseManagementService.Repositories
{
    public interface ISubjectRepository
    {
        public Task<List<Subject>> GetSubjectAsync();
        public Task<Subject> GetSubjectByIdAsync(Guid Code);
        public Task<Subject> CreateSubjectAsync(Subject subject, Guid teacherId);
        public Task<TeacherSubject> DeleteSubjectAsync(Guid Code);
        public Task<Subject> UpdateSubjectAsync(Guid Code, Subject updatedSubject, Guid assignedTeacherId);
    }
}
