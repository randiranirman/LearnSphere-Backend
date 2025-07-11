using System.ComponentModel.DataAnnotations;

namespace CourseRegistration.Application.Dtos
{
    public class StudentCourseRegistrationRequest
    {
        [Required]
        public int ClassId { get; set; }
        
        [Required]
        public List<int> SubjectIds { get; set; } = new List<int>();
    }
}
