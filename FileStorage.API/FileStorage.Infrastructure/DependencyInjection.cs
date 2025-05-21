using FileStorage.Domain.Interfaces;
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
                options.UseSqlServer("Data Source=lmsdatabaseserver.database.windows.net;Initial Catalog=LearnSphereDatabase;User ID=lmsadmin;Password=cop2002@;Trust Server Certificate=True");
            });

            services.AddScoped<ISubjectTopicRepository, SubjectTopicRepository>();

            services.AddScoped<IMetirealRepository, MetirealRepository>();

            services.AddScoped<ITeacherRepository, TeacherRepository>();

            return services;
        }
    }
}
