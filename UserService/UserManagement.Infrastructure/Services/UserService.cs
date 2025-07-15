using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Dtos;
using UserManagement.Application.Repositories;
using UserManagement.Domain.Domain;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Services
{
    public  class UserService : IUserService

    {
        private readonly UserDbContext _context;
        private readonly RedisCacheService _cache;


        private const string userCacheKey = "AllUsers";
        public UserService( UserDbContext context, RedisCacheService cache)
        {
            _context = context;
            _cache = cache;


        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var cachedUsers = await _cache.GetAsync<List<UserDto>>(userCacheKey);

             if( cachedUsers != null)
            {
                return cachedUsers;
            }
            var users = await _context.Users.ToListAsync();
            var userDtos = users.Select(user => new UserDto
            {
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            }).ToList();

            await _cache.SetAsync(userCacheKey, userDtos, TimeSpan.FromMinutes(30));




            return userDtos;
        }


        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return null; ;
            }

            return new UserDto
            {
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };
        }


        public async Task<bool> DeleteUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);


            if (user == null)
            {
                return false;

            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user is null)
            {
                return false;
            }

            _context.Users.Remove(user);


            await _context.SaveChangesAsync();
            return true;




        }

        public async Task<bool> DeleteUserByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                return false;
            }
            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return true;


        }

        public async Task<StudentDto> GetStudentByID(int id)
        {
           var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);

            if ( student ==  null)
            {
                return null;
            }

            return new StudentDto
            {
                StudentID = student.Id,
                StudentName = student.FirstName,
                Email= student.Email
            };

        }

        public async Task<TeacherDto> GetTeacherByID(int id)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == id);

            if( teacher  ==  null)
            {
                return null
                    ;
            }

            return new TeacherDto
            {
                TeacherID = teacher.Id,
                TeacherName= teacher.FullName,
                email = teacher.Email,
            };
        }
    }
}
