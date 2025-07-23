using AnalyticsService.Application.DTOs;
using AnalyticsService.Application.Interfaces;
using MediatR;

namespace AnalyticsService.Application.Querries
{
    public record GetNoOfStudentsAndNoOfTeacherQuery() : IRequest<GetOverviewDTO>;

    public class GetNoOfStudentsAndNoOfTeacherQueryHandler(IAnalyticsRepository analyticsRepository)
        : IRequestHandler<GetNoOfStudentsAndNoOfTeacherQuery, GetOverviewDTO>
    {
        public async Task<GetOverviewDTO> Handle(GetNoOfStudentsAndNoOfTeacherQuery request, CancellationToken cancellationToken)
        {
            return await analyticsRepository.GetNoOfStudentsAndNoOfTeacher();
        }
    }

}
