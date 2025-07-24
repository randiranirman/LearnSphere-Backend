namespace NewAnalyticsServcie.Application.DTOs
{
    public class GetAllSubjectsWithAssignmentsAndSubmissionsByTeacherIdResponseDTO
    {
        public int SubjectId { get; set; }
        public string SubjectTitle { get; set; }
        public string SubjectCode { get; set; }
        public int NoOfAssignments { get; set; }
        public int NoOfRegisterdStudents { get; set; }
    }
}
