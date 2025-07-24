namespace NewAnalyticsService.Domain.Entities
{
    public class Marks
    {
        public int Id { get; set; }
        public int SubmissionId { get; set; }
        public int AssignmentId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public string SubmissionName { get; set; }
        public string AssignmentTitle { get; set; }
        public int AssignmentMarks { get; set; }
    }
}
