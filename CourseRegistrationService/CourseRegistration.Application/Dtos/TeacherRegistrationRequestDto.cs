using System.ComponentModel.DataAnnotations;

namespace CourseRegistration.Application.Dtos
{
    public class TeacherRegistrationRequestDto
    {
        [Required]
        public int TeacherId { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string EmployeeId { get; set; } = string.Empty;
        
        [Required]
        public List<int> ClassIds { get; set; } = new List<int>();
        
        [Required]
        public List<int> SubjectIds { get; set; } = new List<int>();
        
        [MaxLength(500)]
        public string? Remarks { get; set; }
    }
}
