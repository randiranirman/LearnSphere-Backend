using AnalyticsService.Application.DTOs;
using AnalyticsService.Application.Interfaces;
using MediatR;

namespace AnalyticsService.Application.Querries
{
    public record GetTeacherSubejctsWithStudentCountQuery(int teacherId) : IRequest<IEnumerable<SubejctsWithRegisteredStudentsCountDTO>>;

    public class GetTeacherSubejctsWithStudentCountQueryHandler(IAnalyticsRepository analyticsRepository)
        : IRequestHandler<GetTeacherSubejctsWithStudentCountQuery, IEnumerable<SubejctsWithRegisteredStudentsCountDTO>>
    {
        public async Task<IEnumerable<SubejctsWithRegisteredStudentsCountDTO>> Handle(GetTeacherSubejctsWithStudentCountQuery request, CancellationToken cancellationToken)
        {
            return await analyticsRepository.GetTeacherSubejctsWithStudentCount(request.teacherId);
        }
    }
}
