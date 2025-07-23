using FileStorage.Application.Interfaces;
using FileStorage.Infrastructure.Data;
using FileStorage.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileStorage.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraStructureDI(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<FileStorageDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Database"));
            });

            services.AddScoped<ITeacherFilesRepository, TeacherFileRepository>();

            return services;
        }
    }
}
