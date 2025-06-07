using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using MediatR;

namespace FileStorage.Application.Commands
{
    public record DeleteMetirealCommad(int MetirealId) : IRequest<MetirealDTO?>;

    public class DeleteMetirealCommadHandler(IMetirealRepository metirealRepository)
        : IRequestHandler<DeleteMetirealCommad, MetirealDTO?>
    {
        public async Task<MetirealDTO?> Handle(DeleteMetirealCommad request, CancellationToken cancellationToken)
        {
            return await metirealRepository.DeleteMetireal(request.MetirealId);
        }
    }

}
