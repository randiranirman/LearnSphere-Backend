using AnalyticsService.Application;
using AnalyticsService.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace AnalyticsService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            services.AddDomainDI().AddApplicationDI();
            return services;
        }
    }
}
