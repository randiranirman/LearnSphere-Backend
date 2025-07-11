using AnalyticsService.Application;
using AnalyticsService.Domain;
using AnalyticsService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AnalyticsService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            services.AddDbContext<AnalyticsDbContext>(options =>
            {
                options.UseSqlServer("Data Source=localhost\\MSSQLSERVER02;Initial Catalog=AnalyticsDB;Integrated Security=True;Trust Server Certificate=True");
            });
            services.AddDomainDI().AddApplicationDI();
            return services;
        }
    }
}
