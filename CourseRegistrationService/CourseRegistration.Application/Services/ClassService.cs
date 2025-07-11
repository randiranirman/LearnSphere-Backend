using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Application.Dtos;
using CourseRegistration.Application.Interfaces;
using CourseRegistration.Application.Repositories;
using CourseRegistration.Domain.Models;

namespace CourseRegistration.Application.Services
{
    public class ClassService : IClassService

    {
        IClassRepository _classRepository;

        public ClassService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }
        public async Task<Class> CreateClassAsync(CreateClassRequset request)
        {
            var createdClass = new Class
            {
                Name = request.Name,
                Description = request.Description,
                
            };


            await _classRepository.AddAsync(createdClass);

            return createdClass;
        }

        public Task<bool> DeleteClassAsync(int classId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CreateClassRequset>> GetAllClassesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Class> GetClassByIdAsync(int classId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateClassAsync(CreateClassRequset request)
        {
            throw new NotImplementedException();
        }

        
    }
}
