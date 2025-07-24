namespace NewAnalyticsServcie.Application.DTOs
{
    public class GetAllStudentMarksByAssignmentIdResponseDTO
    {
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public int Marks { get; set; }
    }
}
