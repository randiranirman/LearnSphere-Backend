using FileStorage.Domain.Entities;

namespace FileStorage.Domain.Repositories;

public interface IAssignmentRepository
{
    Task<Assignment?> GetAssignmentByIdAsync(int id);
    Task<IEnumerable<Assignment>> GetAllAssignmentsAsync();
    Task<Assignment> AddAssignmentAsync(Assignment assignment);
    Task<Assignment> UpdateAssignmentAsync(Assignment assignment);
    Task<bool> DeleteAssignmentAsync(int id);
}
