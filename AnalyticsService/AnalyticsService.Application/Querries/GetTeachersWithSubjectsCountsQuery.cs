using AnalyticsService.Application.DTOs;
using AnalyticsService.Application.Interfaces;
using MediatR;

namespace AnalyticsService.Application.Querries
{
    public record GetTeachersWithSubjectsCountsQuery() : IRequest<IEnumerable<TeachersWithSubjectsCountDTO>>;

    public class GetTeachersWithSubjectsCountsQueryHandler(IAnalyticsRepository analyticsRepository)
        : IRequestHandler<GetTeachersWithSubjectsCountsQuery, IEnumerable<TeachersWithSubjectsCountDTO>>
    {
        public async Task<IEnumerable<TeachersWithSubjectsCountDTO>> Handle(GetTeachersWithSubjectsCountsQuery request, CancellationToken cancellationToken)
        {
            return await analyticsRepository.GetTeachersWithSubjectsCounts();
        }
    }
}
