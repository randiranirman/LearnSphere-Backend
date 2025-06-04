    using FileStorage.Application;
using FileStorage.Infrastructure;

namespace FileStorage.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services)
        {
            services.AddInfraStructureDI().AddApplicationDI();
            return services;
        }
    }
}
