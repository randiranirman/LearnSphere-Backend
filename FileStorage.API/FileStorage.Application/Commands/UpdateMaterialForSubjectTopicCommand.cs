using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using MediatR;

namespace FileStorage.Application.Commands
{
    public record UpdateMaterialForSubjectTopicCommand(int MaterialId, UpdateMaterialRequestDTO UpdateMaterialRequest)
        : IRequest<MaterialDTO?>;

    public class UpdateMaterialForSubjectTopicCommandHandler(ITeacherFilesRepository teacherFilesRepository)
        : IRequestHandler<UpdateMaterialForSubjectTopicCommand, MaterialDTO?>
    {
        public async Task<MaterialDTO?> Handle(UpdateMaterialForSubjectTopicCommand request, CancellationToken cancellationToken)
        {
            return await teacherFilesRepository.UpdateMaterialForSubjectTopic(request.MaterialId, request.UpdateMaterialRequest);
        }
    }
}
