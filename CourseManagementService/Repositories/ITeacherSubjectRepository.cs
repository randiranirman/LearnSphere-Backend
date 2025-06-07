using CourseManagementService.Models.Domains;

namespace CourseManagementService.Repositories
{
    public interface ITeacherSubjectRepository
    {
        public Task<List<TeacherSubject>> GetTeacherSubjectAsync();
        public Task<TeacherSubject?> GetTeacherSubjectById(Guid subjectCode);
    }
}
