using System.ComponentModel.DataAnnotations;

namespace CourseRegistration.Application.Dtos
{
    public class StudentRegistrationRequestDto
    {
        [Required]
        public int StudentId { get; set; }
        
        [Required]
        public int ClassId { get; set; }
        
        [Required]
        public List<int> SubjectIds { get; set; } = new List<int>();
        
        [Required]
        [MaxLength(20)]
        public string IndexNumber { get; set; } = string.Empty;
    }
}
