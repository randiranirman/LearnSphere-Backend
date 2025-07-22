using System.ComponentModel.DataAnnotations;

namespace AnalyticsService.Domain.Entities
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
