using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Domain;

namespace UserManagement.Application.Repositories
{
    public interface IAdminService
    {

        public Task<bool> EditAdminDetails(int id, Admin request);
        public Task<IEnumerable<Admin>> GetAllAdmins();
        public Task<Admin> GetAdminDetails(int id);
    }
}
