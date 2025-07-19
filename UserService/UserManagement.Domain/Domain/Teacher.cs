using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Domain
{
    public class Teacher
    {
        public int Id { get; set; }

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


        public DateTime DateOfBirth { get; set; }

        [MaxLength(50)]
        public string? EmployeeId { get; set; } // Teacher's employee ID

        [MaxLength(100)]
        public string? Qualification { get; set; } // Teaching qualification

        public DateTime HireDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public string FullName => $"{FirstName} {LastName}";
    }
}
