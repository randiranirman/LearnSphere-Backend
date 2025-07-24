using System.ComponentModel.DataAnnotations;

namespace NewAnalyticsService.Domain.Entities
{
    public class MarkAllocation
    {
        [Key]
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public bool IsMarkAllocated { get; set; } // true -> mark allocated  once : false -> mark not allocated
    }
}
