using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using MediatR;

namespace FileStorage.Application.Commands
{
    public record CreateMetirealCommand(int TopicId, CreateMetirealRequestDTO Metirial) : IRequest<CreateMetirealResponseDTO>;

    public class CreateMetirealCommandHandler(IMetirealRepository metirealRepository)
        : IRequestHandler<CreateMetirealCommand, CreateMetirealResponseDTO>
    {
        public async Task<CreateMetirealResponseDTO> Handle(CreateMetirealCommand request, CancellationToken cancellationToken)
        {
            return await metirealRepository.CreateMetireal(request.TopicId, request.Metirial);
        }
    }
}
