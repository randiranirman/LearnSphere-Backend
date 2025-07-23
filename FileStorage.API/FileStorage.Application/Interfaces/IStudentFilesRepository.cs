using FileStorage.Application.DTOs;

namespace FileStorage.Application.Interfaces
{
    public interface IStudentFilesRepository
    {
        public Task<SubmissionDTO> MadeSubmissionForAssignment(MadeSubmissionByStudentRequestDTO madeSubmissionByStudentRequest);

        public Task<SubmissionDTO?> EditSubmissionForAssignment(int submissionId, EditSubmissionRequestDTO editSubmissionRequest);

        public Task<SubmissionDTO?> DeleteSubmissionBySubmissionId(int submissionId);

        public Task<SubmissionDTO?> GetSubmissionForSpecificStudentAndAssignment(int assignmentId, int studentId);
    }
}
