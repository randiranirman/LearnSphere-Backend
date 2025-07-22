using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using MediatR;

namespace FileStorage.Application.Commands
{
    public record DeleteSubmissionBySubmissionIdCommand(int submissionId) : IRequest<SubmissionDTO?>;

    public class DeleteSubmissionBySubmissionIdCommandHandler(IStudentFilesRepository studentFilesRepository)
        : IRequestHandler<DeleteSubmissionBySubmissionIdCommand, SubmissionDTO?>
    {
        public async Task<SubmissionDTO?> Handle(DeleteSubmissionBySubmissionIdCommand request, CancellationToken cancellationToken)
        {
            var response = await studentFilesRepository.DeleteSubmissionBySubmissionId(request.submissionId);
            if (response is null) return null;
            return response;
        }
    }
}
