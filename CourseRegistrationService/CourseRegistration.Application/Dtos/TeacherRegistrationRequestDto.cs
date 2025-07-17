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

         public string TeacherEmail { get; set; } = string.Empty;
        public int NumberOfStudents { get; set; } = 0;
        public string FirstName { get; set; } = string.Empty;
        public List<string> SubjectCode { get; set; }= new List<string>();

    }
}
