using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CourseRegistration.Application.Dtos
{
    public class StudentCourseRegistrationRequest
    {
        [Required]
        public int ClassId { get; set; }
        
        [Required]
        public List<int> SubjectIds { get; set; } = new List<int>();


        [Required]
        public string fullName { get; set; } = string.Empty;

        [Required]
        public string indexNumber { get; set; } = string.Empty;



        public string address { get; set; } = string.Empty;

    }
}
