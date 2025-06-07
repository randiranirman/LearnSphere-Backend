using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using MediatR;

namespace FileStorage.Application.Querries
{
    public record GetAllMetirealsWithTopicsQuerry() : IRequest<IEnumerable<GetAllTopicsWithMetireals>>;

    public class GetAllMetirealsWithTopicsQuerryHandler(IMetirealRepository metirealRepository)
        : IRequestHandler<GetAllMetirealsWithTopicsQuerry, IEnumerable<GetAllTopicsWithMetireals>>
    {
        public async Task<IEnumerable<GetAllTopicsWithMetireals>> Handle(GetAllMetirealsWithTopicsQuerry request, CancellationToken cancellationToken)
        {
            return await metirealRepository.GetAllMetirealsWithTopicsAsync();
        }
    }
}
