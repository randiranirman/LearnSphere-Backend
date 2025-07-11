using AnalyticsService.Application.DTOs;
using AnalyticsService.Application.Interfaces;
using MediatR;

namespace AnalyticsService.Application.Querries
{
    public record GetAllStudentsBySubjectIdQuerry(int SubjectId) : IRequest<IEnumerable<StudentDTO>>;

    // This query is used to get all students by subject ID.
    public class GetAllStudentsBySubjectIdQuerryHandler(IStudentMarksAnalyticsRepository studentMarksAnalyticsRepository)
        : IRequestHandler<GetAllStudentsBySubjectIdQuerry, IEnumerable<StudentDTO>>
    {
        public async Task<IEnumerable<StudentDTO>> Handle(GetAllStudentsBySubjectIdQuerry request, CancellationToken cancellationToken)
        {
            return await studentMarksAnalyticsRepository.GetAllStudentsAsync(request.SubjectId);
        }
    }

}
