using FileStorage.Application.Interfaces;
using FileStorage.Domain.Entities;
using MediatR;

namespace FileStorage.Application.Querries
{
    public record GetAllTopicsBySubjectIdQuerry(int subjectId) : IRequest<IEnumerable<SubjectTopicEntity>>;
    public class GetAllTopicsBySubjectIdQuerryHandler(ISubjectTopicRepository subjectTopicRepository)
        : IRequestHandler<GetAllTopicsBySubjectIdQuerry, IEnumerable<SubjectTopicEntity>>
    {
        public async Task<IEnumerable<SubjectTopicEntity>> Handle(GetAllTopicsBySubjectIdQuerry request, CancellationToken cancellationToken)
        {
            return await subjectTopicRepository.GetTopicsBySubjectIdAsync(request.subjectId);
        }
    }
}
