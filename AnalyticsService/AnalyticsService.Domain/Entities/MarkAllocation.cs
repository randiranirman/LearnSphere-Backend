namespace AnalyticsService.Domain.Entities
{
    public class MarkAllocation
    {
        public int AssignmentId { get; set; }
        public bool IsMarkAllocated { get; set; } // true -> mark allocated at once : false -> mark not allocated
    }
}
