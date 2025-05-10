using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UserManagement.Application.Dtos;
using UserManagement.Domain.Domain;

namespace UserManagement.Application.Repositories
{
    public interface IAdminService
    {

        public Task<bool> EditAdminDetails(int id, Admin request);
        public Task<IEnumerable<Admin>> GetAllAdmins();
        public Task<Admin> GetAdminDetails(int id);
        public Task<IEnumerable<UserDto>> ReadCsvFile(Stream fileStream);

      
    }
}
