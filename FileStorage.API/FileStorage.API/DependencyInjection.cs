using FileStorage.Application;
using FileStorage.Infrastructure;

namespace FileStorage.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfraStructureDI(configuration).AddApplicationDI();
            return services;
        }
        
    }
}
