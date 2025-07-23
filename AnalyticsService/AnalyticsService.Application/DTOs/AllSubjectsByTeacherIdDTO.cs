namespace AnalyticsService.Application.DTOs
{
    public class AllSubjectsByTeacherIdDTO
    {
        public int SubjectId { get; set; }
        public string SubjectTitle { get; set; }
        public int SubjectGrade { get; set; }
        public int NoOfAssignments { get; set; }
        public int NoOfRegisterdStudents { get; set; }
    }
}
