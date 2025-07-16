namespace AnalyticsService.Application.DTOs
{
    
    public class GetOverviewDTO
    {
        public int NoOfStudents { get; set; }
        public int NoOfTeachers { get; set; }
        public int MinGrade { get; set; }
        public int MaxGrade { get; set; }
    }
}
