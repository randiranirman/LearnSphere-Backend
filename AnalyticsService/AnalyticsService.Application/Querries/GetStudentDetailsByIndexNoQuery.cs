using AnalyticsService.Application.DTOs;
using AnalyticsService.Application.Interfaces;
using MediatR;

namespace AnalyticsService.Application.Querries
{
    public record GetStudentDetailsByIndexNoQuery(string IndexNo) : IRequest<StudentDetailsResponseDTO>;

    public class GetStudentDetailsByIndexNoQueryHandler(IAnalyticsRepository analyticsRepository)
        : IRequestHandler<GetStudentDetailsByIndexNoQuery, StudentDetailsResponseDTO>
    {
        public async Task<StudentDetailsResponseDTO> Handle(GetStudentDetailsByIndexNoQuery request, CancellationToken cancellationToken)
        {
            return await analyticsRepository.GetStudentByIndexNo(request.IndexNo);
        }
    }



}
