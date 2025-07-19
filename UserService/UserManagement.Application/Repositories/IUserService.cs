using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Dtos;
using UserManagement.Domain.Domain;

namespace UserManagement.Application.Repositories
{
    public  interface IUserService
    {

        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<bool> DeleteUserAsync(string username);
        Task<bool> DeleteUserByIdAsync(int id);

        Task<StudentDto> GetStudentByID(int id);
        Task<TeacherDto> GetTeacherByID(int id);
        Task<TeacherDto> EditTeacherDetailsById(int id  , TeacherDto  requset );
        Task<StudentDto> EditStudentDetailsByID(int id, StudentDto request); 
        
        

    }
}
