using CourseManagementService.Models.Domains;

namespace CourseManagementService.Models.DTOs
{
    public class TeacherSubjectDTO
    {
        public Guid TeacherId { get; set; }

        public Guid SubjectCode { get; set; }

        public virtual Subject SubjectCodeNavigation { get; set; } = null!;

        public virtual Teacher Teacher { get; set; } = null!;
    }
}
