using CourseManagementService.Models.Domains;

namespace CourseManagementService.Models.DTOs
{
    public class AddSubjectResponseDTO
    {
        public Guid Code { get; set; }

        public string Title { get; set; } = null!;

        public int Grade { get; set; }

        public Teacher NewlyAssignedTeacher { get; set; }
    }
}
