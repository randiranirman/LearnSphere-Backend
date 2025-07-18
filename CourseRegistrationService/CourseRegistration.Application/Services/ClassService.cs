using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public async Task<bool> DeleteClassAsync(int classId)
        {
            if (classId <= 0)
            {
                throw new ArgumentException("Class ID must be greater than 0", nameof(classId));
            }

            bool result = await _classRepository.DeleteAsync(classId);
            return result;
        }

        public async Task<bool> DeleteClassByIdAsync(int classId)
        {
            if (classId <= 0)
            {
                throw new ArgumentException("Class ID must be greater than 0", nameof(classId));
            }

            bool result = await _classRepository.DeleteAsync(classId);
            return result;
        }

        public async Task<IEnumerable<ClassDto>> GetAllClassesAsync()
        {
            var classes = await _classRepository.GetAllAsync();

            if (classes == null || !classes.Any())
            {
                throw new Exception("No classes found.");
            }

            var classDtos = classes.Select(c => new ClassDto
            {
                Id = c.ClassId,
                Name = c.Name,
                Description = c.Description,
                Code = c.Code,
            });

            return classDtos;
        }
        public async Task<Class?> GetClassByIdAsync(int classId)
        {
            var classToGet = await _classRepository.GetByIdAsync(classId);
            if (classToGet == null)
            {
                return null;
            }

            return classToGet;
        }

        public Task<bool> UpdateClassAsync(CreateClassRequset request)
        {
            throw new NotImplementedException();
        }


    }
}
