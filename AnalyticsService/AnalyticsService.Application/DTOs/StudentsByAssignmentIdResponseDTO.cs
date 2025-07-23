namespace AnalyticsService.Application.DTOs
{
    public class StudentsByAssignmentIdResponseDTO
    {
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public int Marks { get; set; }
    }
}
