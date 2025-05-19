namespace CourseManagementService.Models.DTOs
{
    public class UpdateSubjectRequestDTO
    {
        public string Title { get; set; } = null!;

        public int Grade { get; set; }

        public Guid AssignedTeacherId { get; set; }
    }
}
