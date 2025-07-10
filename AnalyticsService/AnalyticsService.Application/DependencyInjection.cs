using AnalyticsService.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace AnalyticsService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddDomainDI();
            return services;
        }
    }
}