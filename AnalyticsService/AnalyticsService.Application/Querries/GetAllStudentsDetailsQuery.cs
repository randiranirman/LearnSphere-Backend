using AnalyticsService.Application.DTOs;
using AnalyticsService.Application.Interfaces;
using MediatR;

namespace AnalyticsService.Application.Querries
{
    public record GetAllStudentsDetailsQuery() : IRequest<IEnumerable<StudentDetailsResponseDTO>>;

    public class GetAllStudentsDetailsQueryHandler(IAnalyticsRepository analyticsRepository)
        : IRequestHandler<GetAllStudentsDetailsQuery, IEnumerable<StudentDetailsResponseDTO>>
    {
        public async Task<IEnumerable<StudentDetailsResponseDTO>> Handle(GetAllStudentsDetailsQuery request, CancellationToken cancellationToken)
        {
            return await analyticsRepository.GetAllStudentsRegistered();
        }
    }

}
