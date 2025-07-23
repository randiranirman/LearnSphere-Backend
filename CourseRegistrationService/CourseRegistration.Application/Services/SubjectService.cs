using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Application.Dtos;
using CourseRegistration.Application.Interfaces;
using CourseRegistration.Application.Repositories;
using CourseRegistration.Domain.Models;

namespace CourseRegistration.Application.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ICacheService _cacheService;
        private const string CACHE_KEY_PREFIX = "subject_";
        private const string CACHE_KEY_ALL = "subjects_all";
        private const string CACHE_KEY_BY_CODE = "subject_code_";
        private const string CACHE_KEY_BY_STUDENT = "subject_student_";
        private const string CACHE_KEY_BY_TEACHER = "subject_teacher_";
        
        public SubjectService(ISubjectRepository subjectRepository, ICacheService cacheService)
        {
            _subjectRepository = subjectRepository;
            _cacheService = cacheService;
        }

        public async Task<Subject> CreatedSubjectAsync(CreateSubjectRequest request)
        {
            try
            {
                var subject = new Subject
                {
                    SubjectId = request.Id,
                    Name = request.Name,
                    Code = request.Code,
                    Description = request.Description,
                };

                var createdSubject = await _subjectRepository.AddAsync(subject);
                
                // Clear cache after creating new subject
                await _cacheService.RemoveAsync(CACHE_KEY_ALL);
                await _cacheService.RemoveAsync($"{CACHE_KEY_BY_CODE}{subject.Code}");
                
                return createdSubject;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while creating the subject.", ex);
            }
        }

        public async Task<bool> DeleteSubjectAsync(int id)
        {
            try
            {
                // Check if subject exists first
                var existingSubject = await _subjectRepository.GetByIdAsync(id);
                if (existingSubject == null)
                {
                    return false;
                }

                // Delete from repository
                await _subjectRepository.DeleteAsync(id);
                
                // Clear all related cache entries
                await _cacheService.RemoveAsync($"{CACHE_KEY_PREFIX}{id}");
                await _cacheService.RemoveAsync($"{CACHE_KEY_BY_CODE}{existingSubject.Code}");
                await _cacheService.RemoveAsync(CACHE_KEY_ALL);
                
                // Clear student-subject and teacher-subject caches using pattern matching
                // This ensures that any cached subject lists for students/teachers are also cleared
                await _cacheService.RemoveByPatternAsync($"{CACHE_KEY_BY_STUDENT}*");
                await _cacheService.RemoveByPatternAsync($"{CACHE_KEY_BY_TEACHER}*");
                
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while deleting the subject.", ex);
            }
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            // Try to get from cache first
            var cachedSubjects = await _cacheService.GetAsync<List<Subject>>(CACHE_KEY_ALL);
            if (cachedSubjects != null)
            {
                Console.WriteLine("these are the returned data form redis");
                return cachedSubjects;
            }

            // Get from repository if not in cache
            var allSubjects = await _subjectRepository.GetAllAsync();
            var subjectsList = allSubjects.ToList();
            
            // Cache the results
            await _cacheService.SetAsync(CACHE_KEY_ALL, subjectsList, TimeSpan.FromMinutes(30));
            
            return subjectsList;
        }

        public async Task<Subject?> GetSubjectByCodeAsync(string code)
        {
            // Try to get from cache first
            var cacheKey = $"{CACHE_KEY_BY_CODE}{code}";
            var cachedSubject = await _cacheService.GetAsync<Subject>(cacheKey);
            if (cachedSubject != null)
            {
                return cachedSubject;
            }

            // Get from repository if not in cache
            var subject = await _subjectRepository.GetByCodeAsync(code);
            if (subject == null)
            {
                return null;
            }

            // Cache the result
            await _cacheService.SetAsync(cacheKey, subject, TimeSpan.FromMinutes(30));
            
            return subject;
        }

        public async Task<Subject?> GetSubjectByIdAsync(int id)
        {
            // Try to get from cache first
            var cacheKey = $"{CACHE_KEY_PREFIX}{id}";
            var cachedSubject = await _cacheService.GetAsync<Subject>(cacheKey);
            if (cachedSubject != null)
            {
                return cachedSubject;
            }

            // Get from repository if not in cache
            var subject = await _subjectRepository.GetByIdAsync(id);
            if (subject == null)
            {
                return null;
            }

            // Cache the result
            await _cacheService.SetAsync(cacheKey, subject, TimeSpan.FromMinutes(30));
            
            return subject;
        }

        public Task<IEnumerable<Subject>> GetSubjectsByGradeIdAsync(int gradeId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByStudentIdAsync(int studentId)
        {
            // Try to get from cache first
            var cacheKey = $"{CACHE_KEY_BY_STUDENT}{studentId}";
            var cachedSubjects = await _cacheService.GetAsync<List<Subject>>(cacheKey);
            if (cachedSubjects != null)
            {
                return cachedSubjects;
            }

            // Get from repository if not in cache
            var subjects = await _subjectRepository.GetSubjectByStudentIdAsync(studentId);
            if (subjects == null)
            {
                return new List<Subject>();
            }

            var subjectsList = subjects.ToList();
            
            // Cache the result
            await _cacheService.SetAsync(cacheKey, subjectsList, TimeSpan.FromMinutes(15));
            
            return subjectsList;
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByTeacherIdAsync(int teacherId)
        {
            // Try to get from cache first
            var cacheKey = $"{CACHE_KEY_BY_TEACHER}{teacherId}";
            var cachedSubjects = await _cacheService.GetAsync<List<Subject>>(cacheKey);
            if (cachedSubjects != null)
            {
                return cachedSubjects;
            }

            // Get from repository if not in cache
            var subjects = await _subjectRepository.GetSubjectsByTeacherIdAsync(teacherId);
            if (subjects == null)
            {
                return new List<Subject>();
            }

            var subjectsList = subjects.ToList();
            
            // Cache the result
            await _cacheService.SetAsync(cacheKey, subjectsList, TimeSpan.FromMinutes(15));
            
            return subjectsList;
        }

        public async Task<Subject?> UpdateSubjectAsync(int id, CreateSubjectRequest request)
        {
            var existing = await _subjectRepository.GetByIdAsync(id);

            if (existing == null)
            {
                return null;
            }

            // Store old code for cache cleanup
            var oldCode = existing.Code;

            existing.Code = request.Code;
            existing.Name = request.Name;
            existing.Description = request.Description;
            existing.SubjectId = request.Id;

            var updatedSubject = await _subjectRepository.UpdateAsync(existing);
            
            // Clear related cache entries
            await _cacheService.RemoveAsync($"{CACHE_KEY_PREFIX}{id}");
            await _cacheService.RemoveAsync($"{CACHE_KEY_BY_CODE}{oldCode}");
            await _cacheService.RemoveAsync($"{CACHE_KEY_BY_CODE}{request.Code}");
            await _cacheService.RemoveAsync(CACHE_KEY_ALL);

            return updatedSubject;
        }
    }
}
