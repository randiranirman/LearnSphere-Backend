using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using FileStorage.Domain.Entities;
using FileStorage.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Infrastructure.Repositories
{
    public class StudentFileRepository(FileStorageDbContext dbContext) : IStudentFilesRepository
    {
        public async Task<SubmissionDTO?> DeleteSubmissionBySubmissionId(int submissionId)
        {
            var existingDomainModel = await dbContext.Submissions.FirstOrDefaultAsync(s => s.Id == submissionId);
            if (existingDomainModel is null) return null;

            dbContext.Submissions.Remove(existingDomainModel);
            await dbContext.SaveChangesAsync();

            return new SubmissionDTO
            {
                Id = existingDomainModel.Id,
                AssignmentId = existingDomainModel.AssignmentId,
                StudentId = existingDomainModel.StudentId,
                AssignmentDueTime = existingDomainModel.AssignmentDueTime,
                Status = existingDomainModel.Status,
                UploadLink = existingDomainModel.UploadLink,
                SubmissionName = existingDomainModel.SubmissionName,
                SubmitedTime = existingDomainModel.SubmitedTime
            };
        }

        public async Task<SubmissionDTO?> EditSubmissionForAssignment(int submissionId, EditSubmissionRequestDTO editSubmissionRequest)
        {
            var existingDomainModel = await dbContext.Submissions.FirstOrDefaultAsync(s => s.Id == submissionId);

            if (existingDomainModel is null) return null;

            existingDomainModel.SubmissionName = editSubmissionRequest.SubmissionName;
            existingDomainModel.UploadLink = editSubmissionRequest.UploadLink;

            await dbContext.SaveChangesAsync();

            return new SubmissionDTO
            {
                Id = existingDomainModel.Id,
                AssignmentId = existingDomainModel.AssignmentId,
                StudentId = existingDomainModel.StudentId,
                AssignmentDueTime = existingDomainModel.AssignmentDueTime,
                Status = existingDomainModel.Status,
                UploadLink = existingDomainModel.UploadLink,
                SubmissionName = existingDomainModel.SubmissionName,
                SubmitedTime = existingDomainModel.SubmitedTime
            };
        }

        public async Task<SubmissionDTO?> GetSubmissionForSpecificStudentAndAssignment(int assignmentId, int studentId)
        {
            var existingDomainModel = await dbContext.Submissions.FirstOrDefaultAsync(s => s.AssignmentId == assignmentId && s.StudentId == studentId);
            if (existingDomainModel is null) return null;

            return new SubmissionDTO
            {
                Id = existingDomainModel.Id,
                AssignmentId = existingDomainModel.AssignmentId,
                StudentId = existingDomainModel.StudentId,
                AssignmentDueTime = existingDomainModel.AssignmentDueTime,
                Status = existingDomainModel.Status,
                UploadLink = existingDomainModel.UploadLink,
                SubmissionName = existingDomainModel.SubmissionName,
                SubmitedTime = existingDomainModel.SubmitedTime
            };
        }

        public async Task<SubmissionDTO> MadeSubmissionForAssignment(MadeSubmissionByStudentRequestDTO madeSubmissionByStudentRequest)
        {
            var submissionDomainModel = new Submission
            {
                AssignmentId = madeSubmissionByStudentRequest.AssignmentId,
                StudentId = madeSubmissionByStudentRequest.StudentId,
                AssignmentDueTime = madeSubmissionByStudentRequest.AssignmentDueTime,
                UploadLink = madeSubmissionByStudentRequest.UploadLink,
                SubmissionName = madeSubmissionByStudentRequest.SubmissionName,
                SubmitedTime = DateTime.Now
            };

            await dbContext.Submissions.AddAsync(submissionDomainModel);
            await dbContext.SaveChangesAsync();

            return new SubmissionDTO
            {
                Id = submissionDomainModel.Id,
                AssignmentId = submissionDomainModel.AssignmentId,
                StudentId = submissionDomainModel.StudentId,
                AssignmentDueTime = submissionDomainModel.AssignmentDueTime,
                Status = submissionDomainModel.Status,
                UploadLink = submissionDomainModel.UploadLink,
                SubmitedTime = submissionDomainModel.SubmitedTime,
                SubmissionName = submissionDomainModel.SubmissionName
            };
        }
    }
}
