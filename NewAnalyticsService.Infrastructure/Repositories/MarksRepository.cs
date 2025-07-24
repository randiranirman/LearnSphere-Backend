using Microsoft.EntityFrameworkCore;
using NewAnalyticsServcie.Application.DTOs;
using NewAnalyticsServcie.Application.Interfaces;
using NewAnalyticsService.Domain.Entities;
using NewAnalyticsService.Infrastructure.Data;

namespace NewAnalyticsService.Infrastructure.Repositories
{
    public class MarksRepository(NewAnalyticsServiceDbContext newAnalyticsServiceDbContext) : IMarksRepository
    {

        public async Task<Marks> SubmitMarks(int assignmentId, SubmitMarksRequestDTO submitMarksRequest)
        {
            var marksDomainModel = new Marks
            {
                SubmissionId = submitMarksRequest.SubmissionId,
                AssignmentId = assignmentId,
                StudentId = submitMarksRequest.StudentId,
                SubjectId = submitMarksRequest.SubjectId,
                SubmissionName = submitMarksRequest.SubmissionName,
                AssignmentTitle = submitMarksRequest.AssignmentTitle,
                AssignmentMarks = submitMarksRequest.AssignmentMarks
            };

            await newAnalyticsServiceDbContext.Marks.AddAsync(marksDomainModel);
            await newAnalyticsServiceDbContext.SaveChangesAsync();

            return marksDomainModel;
        }

        public async Task<List<Marks>> GetAllSubmissionsMarksByAssignmentId(int assignmentId)
        {
            var result = await newAnalyticsServiceDbContext.Marks
                .Where(m => m.AssignmentId == assignmentId)
                .ToListAsync();

            return result;
        }


        public async Task<Marks> EditMarks(int submissionId, int newMarks)
        {
            var marksDomainModel = await newAnalyticsServiceDbContext.Marks.FirstOrDefaultAsync(m => m.SubmissionId == submissionId);
            if (marksDomainModel is null) return null;
            marksDomainModel.AssignmentMarks = newMarks;

            await newAnalyticsServiceDbContext.SaveChangesAsync();

            return marksDomainModel;
        }

        public async Task<MarksAllocationDTO> GetIsMarkAllocatedStatusByAssignmentId(int assignmentId)
        {
            var result = await newAnalyticsServiceDbContext.MarkAllocations.FirstOrDefaultAsync(m => m.AssignmentId == assignmentId);
            if (result is null) return null;
            return new MarksAllocationDTO
            {
                IsMarksAllocated = result.IsMarkAllocated
            };
        }

        public async Task<MarkAllocation> CreateMarkAllocation(int assignmentId)
        {
            var marksAllocationDomainModel = new MarkAllocation
            {
                AssignmentId = assignmentId,
                IsMarkAllocated = true
            };
            await newAnalyticsServiceDbContext.MarkAllocations.AddAsync(marksAllocationDomainModel);
            await newAnalyticsServiceDbContext.SaveChangesAsync();

            return marksAllocationDomainModel;
        }
    }
}
