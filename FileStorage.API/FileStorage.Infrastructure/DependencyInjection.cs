using FileStorage.Application.Interfaces;
using FileStorage.Infrastructure.Data;
using FileStorage.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FileStorage.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraStructureDI(this IServiceCollection services)
        {

            services.AddDbContext<FileStorageDbContext>(options =>
            {
                options.UseSqlServer("Data Source=localhost\\MSSQLSERVER02;Initial Catalog=LMS;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            });

            services.AddScoped<ISubjectTopicRepository, SubjectTopicRepository>();

            services.AddScoped<IMetirealRepository, MetirealRepository>();

            services.AddScoped<ITeacherRepository, TeacherRepository>();

            return services;
        }
    }
}
