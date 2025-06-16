using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRegistration.Application.Dtos
{
    public class TeacherYearlyRegistrationRequest
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(15)]
        public string ContactNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [MaxLength(50)]
        public string? EmployeeId { get; set; }

        [MaxLength(100)]
        public string? Qualification { get; set; }

        public DateTime HireDate { get; set; }

        [Required]
        public List<int> ClassIds { get; set; } = new List<int>(); // Teacher can register for multiple classes

        [Required]
        public List<int> SubjectIds { get; set; } = new List<int>(); // Teacher selects subjects they can teach

        public int? ExistingTeacherId { get; set; } // For returning teachers
    }
}

