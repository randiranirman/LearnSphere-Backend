using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Repositories;
using UserManagement.Domain.Domain;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Services
{
    public class AdminService(UserDbContext _context) : IAdminService
    {
        public async Task<bool> EditAdminDetails(int id, Admin request)
        {
            var admin = await _context.Admins.FindAsync(id);

            if (admin == null)
            {
                return false;
            }
            admin.FirstName = request.FirstName;
            admin.Address = request.Address;
            admin.LastName = request.LastName;
            admin.ContactNumber = request.ContactNumber;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Admin> GetAdminDetails(int id)
        {
            var admin = await _context.Admins.FindAsync(id);

            if (admin == null)
            {
                return null;
            }
            return admin;
        }

        public async Task<IEnumerable<Admin>> GetAllAdmins()
        {
            var admins = await _context.Admins.ToListAsync();
            return admins;
        }
    }
}
