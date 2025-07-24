using NewAnalyticsService.Application.DTOs;
using NewAnalyticsService.Domain.Entities;

namespace NewAnalyticsService.Application.Interfaces
{
    public interface IMarksRepository
    {
        public Task<List<Marks>> GetAllSubmissionsMarksByAssignmentId(int assignmentId);
        public Task<Marks> SubmitMarks(int assignmentId, SubmitMarksRequestDTO submitMarksRequest);
        public Task<Marks> EditMarks(int submissionId, int newMarks);
        public Task<MarksAllocationDTO> GetIsMarkAllocatedStatusByAssignmentId(int assignmentId);
        public Task<MarkAllocation> CreateMarkAllocation(int assignmentId);
    }
}
