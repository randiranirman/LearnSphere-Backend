using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CourseRegistration.Infrastructure.Data
{
    public  class CourseRegistrationDbContextFactory: IDesignTimeDbContextFactory<CourseRegistrationDbcontext>
    {
        public CourseRegistrationDbcontext CreateDbContext(string[] args)
            
        {
            string connectionString = "Data Source=localhost\\MSSQLSERVER02;Initial Catalog=LearnSphereDatabase;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            var optionsBuilder = new DbContextOptionsBuilder<CourseRegistrationDbcontext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new CourseRegistrationDbcontext(optionsBuilder.Options);
        }
    }
}
