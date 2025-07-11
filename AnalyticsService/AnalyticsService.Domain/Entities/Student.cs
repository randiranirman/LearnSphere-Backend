using System.ComponentModel.DataAnnotations;

namespace AnalyticsService.Domain.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string IndexNumber { get; set; } = string.Empty; // Student's unique index number

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

        [MaxLength(15)]
        public string? ParentContactNumber { get; set; }

        [MaxLength(100)]
        public string? ParentName { get; set; }

        public int Grade { get; set; } // Current grade level

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public string FullName => $"{FirstName} {LastName}";

        public ICollection<Subject?> Subjects { get; set; }
        public ICollection<Assignment?> Assignments { get; set; }
    }
}
