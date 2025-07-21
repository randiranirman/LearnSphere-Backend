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
        private readonly IClassRepository _classRepository;
        private readonly ICacheService _cacheService;
        private const string CACHE_KEY_PREFIX = "class_";
        private const string CACHE_KEY_ALL = "classes_all";
        private const string CACHE_KEY_BY_CODE = "class_code_";

        public ClassService(IClassRepository classRepository, ICacheService cacheService)
        {
            _classRepository = classRepository;
            _cacheService = cacheService;
        }
        public async Task<Class> CreateClassAsync(CreateClassRequset request)
        {
            try
            {
                var createdClass = new Class
                {
                    Name = request.Name,
                    Description = request.Description,
                    Code = request.Code,
                    CreatedAt = DateTime.UtcNow,

                };
                // clear the cache after creating new class 
                await _cacheService.RemoveAsync(CACHE_KEY_ALL);
                await _cacheService.RemoveAsync($"{CACHE_KEY_BY_CODE}{createdClass.Code}");


                await _classRepository.AddAsync(createdClass);

                return createdClass;
            }catch(Exception e )
            {
                throw new Exception("An error occured while creating the class", e);
            }
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

            // Get class before deletion (to access its Code for cache invalidation)
            var classToDelete = await _classRepository.GetByIdAsync(classId);
            if (classToDelete == null)
            {
                throw new Exception($"Class with ID {classId} not found.");
            }

            // Perform deletion
            bool result = await _classRepository.DeleteAsync(classId);

            if (result)
            {
                // Invalidate related cache entries
                await _cacheService.RemoveAsync($"{CACHE_KEY_BY_CODE}{classToDelete.Code}");
                await _cacheService.RemoveAsync(CACHE_KEY_ALL);
                await _cacheService.RemoveAsync($"{CACHE_KEY_PREFIX}{classId}");
            }

            return result;
        }

        public async Task<IEnumerable<ClassDto>> GetAllClassesAsync()
        {
            var cachedClass = await _cacheService.GetAsync<IEnumerable<ClassDto>>(CACHE_KEY_ALL);
            if (cachedClass != null)
            {
                Console.WriteLine("there are the retuned values from cache");
                return cachedClass;
            }
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
            // cache the result 
            await _cacheService.SetAsync(CACHE_KEY_ALL, classDtos,TimeSpan.FromMinutes(30));

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
