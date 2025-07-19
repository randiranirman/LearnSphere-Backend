using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using MediatR;

namespace FileStorage.Application.Commands
{
    public record DeleteAssignmentByTeacherCommand(int AssignmetId) : IRequest<AssignmentDTO>;

    public class DeleteAssignmentByTeacherHandler(ITeacherFilesRepository teacherFilesRepository)
        : IRequestHandler<DeleteAssignmentByTeacherCommand, AssignmentDTO>
    {
        public async Task<AssignmentDTO> Handle(DeleteAssignmentByTeacherCommand request, CancellationToken cancellationToken)
        {
            return await teacherFilesRepository.DeleteAssignmentByTeacher(request.AssignmetId);
        }
    }



}
