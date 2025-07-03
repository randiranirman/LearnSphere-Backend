namespace CourseManagementService.Models.DTOs
{
    public class AddSubjectRequestDTO
    {
        public string Title { get; set; } = null!;
        public int Grade { get; set; }
    }
}
