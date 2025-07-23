namespace AnalyticsService.Application.DTOs
{
    public class SubejctsWithRegisteredStudentsCountDTO
    {
        public int SubjectId { get; set; }
        public string SubjectTitle { get; set; }
        public int Grade { get; set; }
        public int RegisteredStudentCount { get; set; }
    }
}
