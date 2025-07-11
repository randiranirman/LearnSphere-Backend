namespace AnalyticsService.Application.DTOs
{
    public class AllMarksByStudentIdDTO
    {
        public int AssignmentId { get; set; }
        public string AssignmentTitle { get; set; }
        public int Marks { get; set; } // this is the field for store marks => 'A', 'B', 'C', 'S', 'F'
    }
}
