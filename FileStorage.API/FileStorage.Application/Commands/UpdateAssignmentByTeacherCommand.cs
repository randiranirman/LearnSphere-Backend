using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using MediatR;

namespace FileStorage.Application.Commands
{
    public record UpdateAssignmentByTeacherCommand(int AssignmentId, UpdateAssignmentByTeacherRequestDTO UpdateAssignmentByTeacherRequest) : IRequest<AssignmentDTO?>;

    public class UpdateAssignmentByTeacherCommandHandler(ITeacherFilesRepository teacherFilesRepository)
        : IRequestHandler<UpdateAssignmentByTeacherCommand, AssignmentDTO?>
    {
        public async Task<AssignmentDTO?> Handle(UpdateAssignmentByTeacherCommand request, CancellationToken cancellationToken)
        {
            return await teacherFilesRepository.UpdateAssignmentByTeacher(request.AssignmentId, request.UpdateAssignmentByTeacherRequest);
        }
    }
}
