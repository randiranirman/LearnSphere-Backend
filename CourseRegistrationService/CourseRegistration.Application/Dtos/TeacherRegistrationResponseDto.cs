using CourseRegistration.Domain.Models;

namespace CourseRegistration.Application.Dtos
{
    public class TeacherRegistrationResponseDto
    {
        public int TeacherId { get; set; }
        
        public List<int> ClassRegistrationIds { get; set; } = new List<int>();
        public List<int> SubjectRegistrationIds { get; set; } = new List<int>();
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    }
}
