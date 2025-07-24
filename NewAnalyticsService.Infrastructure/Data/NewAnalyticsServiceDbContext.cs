using Microsoft.EntityFrameworkCore;
using NewAnalyticsService.Domain.Entities;

namespace NewAnalyticsService.Infrastructure.Data
{
    public class NewAnalyticsServiceDbContext(DbContextOptions<NewAnalyticsServiceDbContext> options) : DbContext(options)
    {
        public DbSet<Marks> Marks { get; set; }
        public DbSet<MarkAllocation> MarkAllocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Marks>()
                .HasKey(m => m.Id); // now Marks has a primary key

            modelBuilder.Entity<MarkAllocation>()
                .HasKey(ma => ma.Id);
        }

    }
}
