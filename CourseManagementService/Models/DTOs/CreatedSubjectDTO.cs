using CourseManagementService.Models.Domains;

namespace CourseManagementService.Models.DTOs
{
    public class CreatedSubjectDTO
    {
        public Guid Code { get; set; }
        public string Title { get; set; } = null!;
        public int Grade { get; set; }
        public Guid? TeacherID { get; set; }

        public Teacher TeacherDetails { get; set; }
    }
}
