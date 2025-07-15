using System.ComponentModel.DataAnnotations;
using CourseRegistration.Domain.Models;

namespace CourseRegistration.Application.Dtos
{
    public class RegistrationApprovalRequestDto
    {
        [Required]
        public int RegistrationId { get; set; }
        
        [Required]
        public RegistrationStatus Status { get; set; }
        
        [Required]
        public int AdminId { get; set; }
        
        [MaxLength(500)]
        public string? Remarks { get; set; }
    }
}
