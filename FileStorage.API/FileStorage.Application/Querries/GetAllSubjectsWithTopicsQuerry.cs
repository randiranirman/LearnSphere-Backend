using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using MediatR;

namespace FileStorage.Application.Querries
{
    public record GetAllSubjectsWithTopicsQuerry() : IRequest<IEnumerable<SubjectDTO>>;

    public class GetAllSubjectsWithTopicsQuerryHandler(ISubjectTopicRepository subjectTopicRepository)
        : IRequestHandler<GetAllSubjectsWithTopicsQuerry, IEnumerable<SubjectDTO>>
    {
        public async Task<IEnumerable<SubjectDTO>> Handle(GetAllSubjectsWithTopicsQuerry request, CancellationToken cancellationToken)
        {
            return await subjectTopicRepository.GetAllSubjectsWithTopicsAsync();
        }
    }


}
