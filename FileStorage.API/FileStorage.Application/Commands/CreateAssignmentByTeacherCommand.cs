using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using MediatR;

namespace FileStorage.Application.Commands
{
    public record CreateAssignmentByTeacherCommand(CreateAssignmentByTeacherRequestDTO CreateAssignmentByTeacherRequest) : IRequest<AssignmentDTO>;

    public class CreateAssignmentByTeacherCommandHandler(ITeacherFilesRepository teacherFilesRepository)
        : IRequestHandler<CreateAssignmentByTeacherCommand, AssignmentDTO>
    {
        public async Task<AssignmentDTO> Handle(CreateAssignmentByTeacherCommand request, CancellationToken cancellationToken)
        {
            return await teacherFilesRepository.CreateAssignmentByTeacher(request.CreateAssignmentByTeacherRequest);
        }
    }

}
