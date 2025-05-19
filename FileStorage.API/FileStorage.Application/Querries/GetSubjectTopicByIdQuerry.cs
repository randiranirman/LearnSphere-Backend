using FileStorage.Domain.Entities;
using FileStorage.Domain.Interfaces;
using MediatR;

namespace FileStorage.Application.Querries
{
    public record GetSubjectTopicByIdQuerry(int topicId) : IRequest<SubjectTopicEntity>;
    public class GetSubjectTopicByIdQuerryHandler(ISubjectTopicRepository subjectTopicRepository)
        : IRequestHandler<GetSubjectTopicByIdQuerry, SubjectTopicEntity>
    {
        public async Task<SubjectTopicEntity?> Handle(GetSubjectTopicByIdQuerry request, CancellationToken cancellationToken)
        {
            return await subjectTopicRepository.GetTopicByIdAsync(request.topicId);
        }
    }
}
