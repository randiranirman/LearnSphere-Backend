using FileStorage.API.mappings;
using FileStorage.Application;
using FileStorage.Infrastructure;

namespace FileStorage.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services)
        {
            services.AddInfraStructureDI().AddApplicationDI();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            return services;
        }
        
    }
}
