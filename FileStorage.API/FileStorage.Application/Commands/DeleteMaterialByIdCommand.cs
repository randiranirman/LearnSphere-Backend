using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using MediatR;

namespace FileStorage.Application.Commands
{
    public record DeleteMaterialByIdCommand(int MaterialId) : IRequest<MaterialDTO?>;

    public class DeleteMaterialByIdCommandHandler(ITeacherFilesRepository teacherFilesRepository)
        : IRequestHandler<DeleteMaterialByIdCommand, MaterialDTO?>
    {
        public async Task<MaterialDTO?> Handle(DeleteMaterialByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await teacherFilesRepository.DeleteMaterialByMaterialId(request.MaterialId);
            if (response is null) return null;
            return response;
        }
    }
}
