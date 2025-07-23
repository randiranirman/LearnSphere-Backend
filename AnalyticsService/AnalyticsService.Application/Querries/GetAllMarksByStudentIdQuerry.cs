using AnalyticsService.Application.DTOs;
using AnalyticsService.Application.Interfaces;
using MediatR;

namespace AnalyticsService.Application.Querries
{
    public record GetAllMarksByStudentIdQuerry(int subjectId, int studentId) : IRequest<IEnumerable<AllMarksByStudentIdDTO>>;

    // This query is used to get all marks for a specific student by their ID.
    public class GetAllMarksByStudentIdQuerryHandler(IStudentMarksAnalyticsRepository studentMarksAnalyticsRepository) 
        : IRequestHandler<GetAllMarksByStudentIdQuerry, IEnumerable<AllMarksByStudentIdDTO>>
    {
        public async Task<IEnumerable<AllMarksByStudentIdDTO>> Handle(GetAllMarksByStudentIdQuerry request, CancellationToken cancellationToken)
        {
            // Fetch all marks for the student with the given ID
            return await studentMarksAnalyticsRepository.GetAllMarksByStudentIdAsync(request.subjectId, request.studentId);
        }
    }
}
