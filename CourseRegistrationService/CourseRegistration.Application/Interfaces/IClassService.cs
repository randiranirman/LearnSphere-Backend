using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Application.Dtos;
using CourseRegistration.Domain.Models;

namespace CourseRegistration.Application.Interfaces
{
    public  interface IClassService
    {

        Task<Class> CreateClassAsync(CreateClassRequset request);
        Task<bool> UpdateClassAsync(CreateClassRequset request);
        Task<bool> DeleteClassAsync(int classId);
        Task<bool> DeleteClassByIdAsync(int classId);
        Task<IEnumerable<ClassDto>> GetAllClassesAsync();
        Task<Class?> GetClassByIdAsync(int classId);

      
    }
}
