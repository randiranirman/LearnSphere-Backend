namespace CourseManagementService.Models.DTOs
{
    public class AddTeacherRequestDTO
    {
        public string FullName { get; set; } = null!;

        public DateOnly? Birthday { get; set; }

        public string Email { get; set; } = null!;

        public string ContactNo { get; set; } = null!;

        public string? Address { get; set; }
    }
}
