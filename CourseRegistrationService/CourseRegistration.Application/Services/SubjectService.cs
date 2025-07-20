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
        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
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

                return await _subjectRepository.AddAsync(subject);
            }catch( Exception ex)
            {

                // Log the exception or handle it as needed
                throw new Exception("An error occurred while creating the subject.", ex);
            }






        }

        public Task<bool> DeleteSubjectAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            var allSubjects =await  _subjectRepository.GetAllAsync();
            return allSubjects;
        }

        public async  Task<Subject?> GetSubjectByCodeAsync(string code)
        {
            var subject = await _subjectRepository.GetByCodeAsync(code);
            if( subject == null)
            {
                return null;
            }

            return subject;
        }

        public async Task<Subject?> GetSubjectByIdAsync(int id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            if(  subject == null)
            {
                return null;
            }

            return subject;
        }

        public Task<IEnumerable<Subject>> GetSubjectsByGradeIdAsync(int gradeId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByStudentIdAsync(int studentId)
        {
            var subjects = await _subjectRepository.GetSubjectByStudentIdAsync(studentId);

            if (subjects == null)
            {
                return new List<Subject>();
            }

            return subjects;
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByTeacherIdAsync(int teacherId)
        {
            var subjects = await _subjectRepository.GetSubjectsByTeacherIdAsync(teacherId);
            if (subjects == null)
            {
                return new List<Subject>();
            }

            return subjects;
        }

        public async Task<Subject?> UpdateSubjectAsync(int id, CreateSubjectRequest request)
        {
            var existing = await  _subjectRepository.GetByIdAsync(id);

            if( existing == null)
            {
                return null;
            }

            existing.Code = request.Code;
            existing.Name = request.Name;
            existing.Description = request.Description;

            existing.SubjectId = request.Id;


            return existing;
            
        }
    }
}
