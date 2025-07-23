using System.ComponentModel.DataAnnotations;

namespace CourseRegistration.Domain.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Message { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } = "info"; // info, success, warning, error

        [Required]
        public int UserId { get; set; } // The user who should receive this notification

        [Required]
        [MaxLength(20)]
        public string UserRole { get; set; } = string.Empty; // Student, Teacher, Admin

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ReadAt { get; set; }

        // Optional: For specific notification categories
        [MaxLength(50)]
        public string? Category { get; set; } // Registration, Assignment, System, etc.

        // Optional: Reference ID for related entities
        public int? ReferenceId { get; set; }

        // Optional: Reference type (Registration, Assignment, etc.)
        [MaxLength(50)]
        public string? ReferenceType { get; set; }

        // Optional: Action URL if notification is clickable
        [MaxLength(500)]
        public string? ActionUrl { get; set; }

        // Optional: Additional data as JSON
        public string? Data { get; set; }

        public bool IsDeleted { get; set; } = false;
        public int TargetUserId { get; set; } // The user who should receive this notification
        public string TargetRole { get; set; } =  string.Empty; // The role of the user who should receive this notification
    }
}
