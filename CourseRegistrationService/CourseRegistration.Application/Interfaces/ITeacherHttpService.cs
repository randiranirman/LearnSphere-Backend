using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Application.Dtos;

namespace CourseRegistration.Application.Interfaces
{
    public interface ITeacherHttpService
    {

        Task<TeacherDto?> GetTeacherByIdAsync (int id);

        Task<bool> ValidateStudentExistsAsync(int studentId);


        Task<List<TeacherDto>> GetAllTeachersAsync();

    }
}
