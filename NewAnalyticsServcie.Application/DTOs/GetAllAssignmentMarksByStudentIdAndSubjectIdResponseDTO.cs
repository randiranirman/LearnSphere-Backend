namespace NewAnalyticsServcie.Application.DTOs
{
    public class GetAllAssignmentMarksByStudentIdAndSubjectIdResponseDTO
    {
        public int AssignmentId { get; set; }
        public string AssignmentTitle { get; set; }
        public int Marks { get; set; }
    }
}
