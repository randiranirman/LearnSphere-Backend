namespace NewAnalyticsService.Application.DTOs
{
    public class SubmitMarksRequestDTO
    {
        public int SubmissionId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public string SubmissionName { get; set; }
        public string AssignmentTitle { get; set; }
        public int AssignmentMarks { get; set; }
    }
}
