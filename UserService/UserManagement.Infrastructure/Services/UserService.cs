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
            List<UserDto> cachedUsers = null;

            // Try to get from cache (but don't crash if Redis is down)
            try
            {
                cachedUsers = await _cache.GetAsync<List<UserDto>>(userCacheKey);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Redis Get] Failed to get from cache: {ex.Message}");
            }

            // If cache hit, return cached data
            if (cachedUsers != null)
            {
                Console.WriteLine("Returning users from Redis cache.");
                return cachedUsers;
            }

            // If cache miss or Redis failed, go to DB
            var users = await _context.Users.ToListAsync();
            var userDtos = users.Select(user => new UserDto
            {
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            }).ToList();

            // Try to cache the result (again, don't crash if Redis is down)
            try
            {
                await _cache.SetAsync(userCacheKey, userDtos, TimeSpan.FromMinutes(30));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Redis Set] Failed to cache data: {ex.Message}");
            }

            return userDtos;
        }




        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            string cacheKey = $"User_{id}";
            var cachedUser = await _cache.GetAsync<UserDto>(cacheKey);
            if(cachedUser != null)
            {
                return cachedUser;
            }


            var user = await _context.Users.FindAsync(id);


            if (user == null)
            {
                return null; ;
            }
            var userDto = new UserDto
            {
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };
            await _cache.SetAsync(cacheKey, userDto, TimeSpan.FromMinutes(30));

            return userDto;
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
