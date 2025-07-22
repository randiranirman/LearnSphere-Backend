using AnalyticsService.Application;
using AnalyticsService.Domain;
using AnalyticsService.Infrastructure;

namespace AnalyticsService.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAnalyticsServiceAPI(this IServiceCollection services)
        {
            services.AddApplicationDI().AddDomainDI().AddInfrastructureDI();
            return services;
        }
    }
}
