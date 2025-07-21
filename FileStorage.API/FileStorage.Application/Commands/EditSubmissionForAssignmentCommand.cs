using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using MediatR;

namespace FileStorage.Application.Commands
{
    public record EditSubmissionForAssignmentCommand(int submissionId, EditSubmissionRequestDTO EditSubmissionRequest) : IRequest<SubmissionDTO?>;

    public class EditSubmissionForAssignmentCommandHandler(IStudentFilesRepository studentFilesRepository)
        : IRequestHandler<EditSubmissionForAssignmentCommand, SubmissionDTO?>
    {
        public async Task<SubmissionDTO?> Handle(EditSubmissionForAssignmentCommand request, CancellationToken cancellationToken)
        {
            var response = await studentFilesRepository.EditSubmissionForAssignment(request.submissionId, request.EditSubmissionRequest);
            if (response is null) return null;
            return response;
        }
    }

}
