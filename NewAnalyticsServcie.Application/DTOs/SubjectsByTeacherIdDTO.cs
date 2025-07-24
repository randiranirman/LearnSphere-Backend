namespace NewAnalyticsServcie.Application.DTOs
{
    public class SubjectsByTeacherIdDTO
    {
        public int SubjectId { get; set; }
            
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
