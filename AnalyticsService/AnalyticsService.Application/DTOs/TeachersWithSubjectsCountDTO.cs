namespace AnalyticsService.Application.DTOs
{
    public class TeachersWithSubjectsCountDTO
    {
        public int TeacherId { get; set; }
        public string EmployeeId { get; set; }
        public string TeacherFullName { get; set; }
        public string TeacherEmail { get; set; }
        public int SubjectCount { get; set; }

    }
}
