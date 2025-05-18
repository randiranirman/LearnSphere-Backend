using FileStorage.Domain.Entities;
using FileStorage.Domain.Interfaces;
using MediatR;

namespace FileStorage.Application.Querries
{
    public record GetAllMetirealsByTopicIdQuerry(int topicId) : IRequest<IEnumerable<MetirialEntity>>;

    public class GetAllMetirealsByTopicIdQuerryHandler(IMetirealRepository metirealRepository)
        : IRequestHandler<GetAllMetirealsByTopicIdQuerry, IEnumerable<MetirialEntity>>
    {
        public async Task<IEnumerable<MetirialEntity>> Handle(GetAllMetirealsByTopicIdQuerry request, CancellationToken cancellationToken)
        {
            return await metirealRepository.GetAllMetirealsByTopicId(request.topicId);
        }
    }
}
