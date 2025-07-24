using FileStorage.Application.DTOs;
using MediatR.Pipeline;

namespace FileStorage.Application.Interfaces
{
    public interface IAssignmentRepository
    {
        public Task<IEnumerable<GetAllAssignmentBySubjectIdResponse>> GetAllAssignmentDetailsFromSubjectId(int subjectId);

        public Task<int> GetAssignmentCountBySubjectIdAsync(int subjectId);
    }
}
