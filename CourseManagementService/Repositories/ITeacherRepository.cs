using CourseManagementService.Models.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace CourseManagementService.Repositories
{
    public interface ITeacherRepository
    {
        public Task<List<Teacher>> GetAllAsync();
        public Task<Teacher> GetByIdAsync(Guid Id);
        public Task<Teacher> CreateAsync(Teacher teacher);
        public Task<Teacher> DeleteAsync(Guid Id);
        public Task<Teacher> UpdateAsync(Guid Id, Teacher updateTeacher);
    }
}
