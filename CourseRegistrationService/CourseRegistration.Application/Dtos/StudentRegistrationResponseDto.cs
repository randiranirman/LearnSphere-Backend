using CourseRegistration.Domain.Models;

namespace CourseRegistration.Application.Dtos
{
    public class StudentRegistrationResponseDto
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public List<int> RegistrationIds { get; set; } = new List<int>();
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
