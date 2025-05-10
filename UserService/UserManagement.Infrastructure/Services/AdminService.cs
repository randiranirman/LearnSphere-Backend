using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.TypeConversion;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Dtos;
using UserManagement.Application.Repositories;
using UserManagement.Domain.Domain;
using UserManagement.Domain.Repositories;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Services
{
    public class AdminService(UserDbContext _context, IAuthService _authService) : IAdminService
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

        public  async Task<IEnumerable<UserDto>> ReadCsvFile(Stream fileStream)
        {
            try
            {
                using (var reader = new StreamReader(fileStream))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<UserDto>();
                    return records.ToList();

                }
            }
            catch (HeaderValidationException ex)
            {
                throw new ApplicationException("CSV file hreader is invalid ");
            }
            catch (TypeConverterException ex) {

                throw new ApplicationException("Csv files contains invalid data format" + ex);

            }catch( Exception ex)
            {
                throw new Exception(" something went wrong " +  ex);

            }
        }
    }
}
