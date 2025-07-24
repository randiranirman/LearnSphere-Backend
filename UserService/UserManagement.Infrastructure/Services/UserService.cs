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
            // Try to get from cache using enhanced method
            var (cachedUsers, cacheSuccess) = await _cache.TryGetAsync<List<UserDto>>(userCacheKey);
            
            if (cacheSuccess && cachedUsers != null)
            {
                Console.WriteLine("Returning users from Redis cache.");
                return cachedUsers;
            }

            // If cache miss or Redis failed, go to DB
            Console.WriteLine("Cache miss - fetching users from database.");
            var users = await _context.Users.ToListAsync();
            var userDtos = users.Select(user => new UserDto
            {
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            }).ToList();

            // Try to cache the result using enhanced method
            await _cache.TrySetAsync(userCacheKey, userDtos, TimeSpan.FromMinutes(30));

            return userDtos;
        }




        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            string cacheKey = $"User_{id}";
            
            // Try to get from cache using enhanced method
            var (cachedUser, cacheSuccess) = await _cache.TryGetAsync<UserDto>(cacheKey);
            if (cacheSuccess && cachedUser != null)
            {
                Console.WriteLine($"Returning user {id} from Redis cache.");
                return cachedUser;
            }

            // Cache miss - fetch from database
            Console.WriteLine($"Cache miss - fetching user {id} from database.");
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            var userDto = new UserDto
            {
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };
            
            // Cache the result using enhanced method
            await _cache.TrySetAsync(cacheKey, userDto, TimeSpan.FromMinutes(30));

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

            // Invalidate cache after successful deletion using enhanced method
            await _cache.RemoveMultipleAsync(userCacheKey, $"User_{id}");
            Console.WriteLine($"Cache invalidated for user ID: {id}");

            return true;
        }

        public async Task<bool> DeleteUserAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user is null)
            {
                return false;
            }

            int userId = user.Id; // Store ID before deletion for cache invalidation
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            // Invalidate cache after successful deletion using enhanced method
            await _cache.RemoveMultipleAsync(userCacheKey, $"User_{userId}");
            Console.WriteLine($"Cache invalidated for user: {username} (ID: {userId})");

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

            // Invalidate cache after successful deletion using enhanced method
            await _cache.RemoveMultipleAsync(userCacheKey, $"User_{id}");
            Console.WriteLine($"Cache invalidated for user ID: {id}");

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
                TeacherName = teacher.FullName,
                Email = teacher.Email,
                ContactNumber = teacher.ContactNumber
            };
        }

        async Task<TeacherDto> IUserService.EditTeacherDetailsById(int id, TeacherDto requset)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == id);
            if (teacher == null)
            {
                return null;
            }

            teacher.Email = requset.Email;
            teacher.ContactNumber = requset.ContactNumber;

            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync();

            // Invalidate cache after successful update using enhanced method
            await _cache.TryRemoveAsync($"Teacher_{id}");
            Console.WriteLine($"Cache invalidated for teacher ID: {id}");

            return new TeacherDto
            {
                TeacherID = teacher.Id,
                ContactNumber = teacher.ContactNumber,
                TeacherName = teacher.FullName,
                Email = teacher.Email,
            };
        }

        async Task<StudentDto> IUserService.EditStudentDetailsByID(int id, StudentDto request)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
            {
                return null;
            }

            student.FirstName = request.StudentName;
            student.Email = request.Email;
            student.ContactNumber = request.ContactNumber;

            _context.Students.Update(student);
            await _context.SaveChangesAsync();

            // Invalidate cache after successful update using enhanced method
            await _cache.TryRemoveAsync($"Student_{id}");
            Console.WriteLine($"Cache invalidated for student ID: {id}");

            return new StudentDto
            {
                StudentID = student.Id,
                StudentName = student.FirstName,
                Email = student.Email,
                ContactNumber = student.ContactNumber
            };
        }

        async Task<IEnumerable<StudentDto>> IUserService.getDeatilsStudentsByIds(List<int> studentIDs)
        {
            var studentDetails = await _context.Students
                    .Where(s => studentIDs.Contains(s.Id)) // Id is the foreign key from Student to User
                    .Join(_context.Students,
                          student => student.Id,
                          user => user.Id,
                          (student, user) => new StudentDto
                          {
                              StudentID = student.Id,
                              StudentName = student.FullName,
                              Email = user.Email,
                              ContactNumber = user.ContactNumber,
                              IndexNumber = user.IndexNumber,
                              DateOfBirth = user.DateOfBirth,
                              Address = user.Address,
                              ParentContactNumber = user.ParentContactNumber,
                              ParentName = user.ParentName,
                              Grade = user.Grade
                          })
                    .ToListAsync();

            return studentDetails;
        }

        async Task<GetNoOfTeachersAndNoOfStudentsResponseDTO> IUserService.GetNoOfTeachersAndNoOfStudents()
        {
            var noOfStudents = await _context.Students.CountAsync();
            var noOfTeachers = await _context.Teachers.CountAsync();

            return new GetNoOfTeachersAndNoOfStudentsResponseDTO
            {
                NoOfStudents = noOfStudents,
                NoOfTeachers = noOfTeachers
            };
        }

        async Task<IEnumerable<GetAllTeachersResponseDTO>> IUserService.GetAllTeachersRegistered()
        {
            var result = await _context.Teachers
                .Select(t => new GetAllTeachersResponseDTO
                {
                    TeacherID = t.Id,
                    TeacherName = $"{t.FirstName} {t.LastName}",
                    ContactNumber = t.ContactNumber,
                    EmployeeId = t.EmployeeId ?? string.Empty,
                    Email = t.Email
                })
                .ToListAsync();

            return result;
        }

        async Task<IEnumerable<GetAllStudentsRegisteredResponseDTO>> IUserService.GetAllStudentsRegistered()
        {
            var result = await _context.Students
                .Select(s => new GetAllStudentsRegisteredResponseDTO
                {
                    Id = s.Id,
                    IndexNo = s.IndexNumber,
                    FullName = $"{s.FirstName} {s.LastName}",
                    Grade = s.Grade
                })
                .ToListAsync();

            return result;
        }

        async Task<GetAllStudentsRegisteredResponseDTO> IUserService.GetStudentByIndexNoAsync(string indexNo)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.IndexNumber == indexNo);

            if (student == null)
                return null;

            return new GetAllStudentsRegisteredResponseDTO
            {
                Id = student.Id,
                IndexNo = student.IndexNumber,
                FullName = $"{student.FirstName} {student.LastName}",
                Grade = student.Grade
            };
        }
    }
}
