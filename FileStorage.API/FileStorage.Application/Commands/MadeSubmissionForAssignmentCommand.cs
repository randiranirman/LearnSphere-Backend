using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using FileStorage.Domain.Entities;
using MediatR;

namespace FileStorage.Application.Commands
{
    public record MadeSubmissionForAssignmentCommand(MadeSubmissionByStudentRequestDTO MadeSubmissionByStudentRequest) : IRequest<SubmissionDTO>;

    public class MadeSubmissionForAssignmentCommandHandler(IStudentFilesRepository studentFilesRepository)
        : IRequestHandler<MadeSubmissionForAssignmentCommand, SubmissionDTO>
    {
        public async Task<SubmissionDTO> Handle(MadeSubmissionForAssignmentCommand request, CancellationToken cancellationToken)
        {
            return await studentFilesRepository.MadeSubmissionForAssignment(request.MadeSubmissionByStudentRequest);
        }
    }


}
