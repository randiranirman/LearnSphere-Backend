using System.ComponentModel.DataAnnotations;

namespace CourseRegistration.Application.Dtos
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = "info";
        public int UserId { get; set; }
        public string UserRole { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ReadAt { get; set; }
        public string? Category { get; set; }
        public int? ReferenceId { get; set; }
        public string? ReferenceType { get; set; }
        public string? ActionUrl { get; set; }
        public string? Data { get; set; }
    }

    public class CreateNotificationDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Message { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Type { get; set; } = "info";

        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(20)]
        public string UserRole { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Category { get; set; }

        public int? ReferenceId { get; set; }

        [MaxLength(50)]
        public string? ReferenceType { get; set; }

        [MaxLength(500)]
        public string? ActionUrl { get; set; }

        public string? Data { get; set; }
    }

    public class UpdateNotificationDto
    {
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
    }

    public class NotificationQueryDto
    {
        public int UserId { get; set; }
        public string? UserRole { get; set; }
        public bool? IsRead { get; set; }
        public string? Category { get; set; }
        public string? Type { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public class MarkAsReadDto
    {
        [Required]
        public List<int> NotificationIds { get; set; } = new List<int>();
    }

    public class BulkNotificationDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Message { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Type { get; set; } = "info";

        [MaxLength(50)]
        public string? Category { get; set; }

        [MaxLength(500)]
        public string? ActionUrl { get; set; }

        public string? Data { get; set; }

        // For role-based notifications
        public List<string>? TargetRoles { get; set; }

        // For specific user notifications
        public List<int>? TargetUserIds { get; set; }
    }
}
