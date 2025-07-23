using AnalyticsService.Application.DTOs;
using AnalyticsService.Application.Interfaces;
using MediatR;

namespace AnalyticsService.Application.Querries
{
    public record GetAllSubjectsByTeacherIdQuery(int TeacherId) : IRequest<IEnumerable<AllSubjectsByTeacherIdDTO>>;

    public class GetAllSubjectsByTeacherIdQueryHandler(ITeacherRepository teacherRepository)
        : IRequestHandler<GetAllSubjectsByTeacherIdQuery, IEnumerable<AllSubjectsByTeacherIdDTO>>
    {
        public async Task<IEnumerable<AllSubjectsByTeacherIdDTO>> Handle(GetAllSubjectsByTeacherIdQuery request, CancellationToken cancellationToken)
        {
            return await teacherRepository.GetAllSubjectsByTeacherIdAsync(request.TeacherId);
        }
    }
}
