using FileStorage.Application.Interfaces;
using FileStorage.Domain.Entities;
using MediatR;

namespace FileStorage.Application.Commands
{
    public record CreateMetirealCommand(int TopicId, MetirialEntity Metirial) : IRequest<MetirialEntity>;

    public class CreateMetirealCommandHandler(IMetirealRepository metirealRepository)
        : IRequestHandler<CreateMetirealCommand, MetirialEntity>
    {
        public async Task<MetirialEntity> Handle(CreateMetirealCommand request, CancellationToken cancellationToken)
        {
            return await metirealRepository.CreateMetireal(request.TopicId, request.Metirial);
        }
    }
}
