using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRegistration.Application.Dtos
{
    public class StudentYearlyRegistrationRequest
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string IndexNumber { get; set; } = string.Empty;

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

        [MaxLength(15)]
        public string? ParentContactNumber { get; set; }

        [MaxLength(100)]
        public string? ParentName { get; set; }

        [Required]
        public int Grade { get; set; }

        [Required]
        public int ClassId { get; set; } // Student selects ONE class

        [Required]
        public List<int> SubjectIds { get; set; } = new List<int>(); // Student selects subjects

        public int? ExistingStudentId { get; set; } // For returning students
    }
}

