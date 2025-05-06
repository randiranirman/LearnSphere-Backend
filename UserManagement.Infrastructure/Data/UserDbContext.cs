
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Domain;

namespace UserManagement.Infrastructure.Data
{
    public  class UserDbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public Dbset<Teacher> Teachers { get; set; }
        public Dbset<Student> Students { get; set; }

    }
}
