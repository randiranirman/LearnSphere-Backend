using FileStorage.Domain.Entities;
using FileStorage.Domain.Interfaces;
using MediatR;

namespace FileStorage.Application.Querries
{
    public record GetAllSubjectsWithTopicsQuerry() : IRequest<IEnumerable<SubjectEntity>>;

    public class GetAllSubjectsWithTopicsQuerryHandler(ISubjectTopicRepository subjectTopicRepository)
        : IRequestHandler<GetAllSubjectsWithTopicsQuerry, IEnumerable<SubjectEntity>>
    {
        public async Task<IEnumerable<SubjectEntity>> Handle(GetAllSubjectsWithTopicsQuerry request, CancellationToken cancellationToken)
        {
            return await subjectTopicRepository.GetAllSubjectsWithTopicsAsync();
        }
    }


}
