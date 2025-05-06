using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Dtos;
using UserManagement.Application.Repositories;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Services
{
    public  class UserService : IUserService

    {
        private readonly UserDbContext _context;
        public UserService( UserDbContext context)
        {
            _context = context;


        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();

            return users.Select(user => new UserDto
            {
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            }).ToList();
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

    }
}
