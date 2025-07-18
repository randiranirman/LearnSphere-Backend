using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using MediatR;

namespace FileStorage.Application.Commands
{
    public record CreateNewMaterialCommand(int TopicId, CreateMaterialRequestDTO CreateMaterialRequest) : IRequest<CreateMaterialResponseDTO>;

    public class CreateNewMaterialCommandHandler(ITeacherFilesRepository teacherFilesRepository)
        : IRequestHandler<CreateNewMaterialCommand, CreateMaterialResponseDTO>
    {
        public async Task<CreateMaterialResponseDTO> Handle(CreateNewMaterialCommand request, CancellationToken cancellationToken)
        {
            return await teacherFilesRepository.CreateMaterialForTopics(request.TopicId, request.CreateMaterialRequest);
        }
    }

}
