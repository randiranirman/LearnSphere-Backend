using Microsoft.Extensions.DependencyInjection;

namespace AnalyticsService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly)); // add dependency for the mediator pattern
            return services;
        }
    }
}