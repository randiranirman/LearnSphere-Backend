using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using MediatR;

namespace FileStorage.Application.Commands
{
    public record UpdateMetirealSavedNameCommand(int MetirealId, UpdateMetirealSavedNameDTO SavedNameDTO) : IRequest<MetirealDTO?>;

    public class UpdateMetirealSavedNameCommandHandler(IMetirealRepository metirealRepository)
        : IRequestHandler<UpdateMetirealSavedNameCommand, MetirealDTO?>
    {
        public async Task<MetirealDTO?> Handle(UpdateMetirealSavedNameCommand request, CancellationToken cancellationToken)
        {
            return await metirealRepository.UpdateMetirealSavedName(request.MetirealId, request.SavedNameDTO);
        }
    }

}
