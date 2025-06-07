namespace CourseManagementService.Models.DTOs
{
    public class SubjectDTO
    {
        public Guid Code { get; set; }

        public string Title { get; set; } = null!;

        public int Grade { get; set; }
    }
}
