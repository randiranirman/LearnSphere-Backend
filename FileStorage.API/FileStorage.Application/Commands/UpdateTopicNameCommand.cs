using FileStorage.Application.DTOs;
using FileStorage.Application.Interfaces;
using MediatR;

namespace FileStorage.Application.Commands
{
    public record UpdateTopicNameCommand(int TopicId, string NewTopicName) : IRequest<TopicDTO?>;

    public class UpdateTopicNameCommandHandler(ISubjectTopicRepository subjectTopicRepository)
        : IRequestHandler<UpdateTopicNameCommand, TopicDTO?>
    {
        public async Task<TopicDTO?> Handle(UpdateTopicNameCommand request, CancellationToken cancellationToken)
        {
            return await subjectTopicRepository.EditTopicNameAsync(request.TopicId, request.NewTopicName);
        }
    }

}
