
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Domain;

namespace UserManagement.Infrastructure.Data
{
    public  class UserDbContext(DbContextOptions<UserDbContext> options): DbContext(options)
   
    {
        

        public DbSet<User>? Users { get; set; }
        public DbSet<Admin>? Admins { get; set; }
       public DbSet<Teacher>? Teachers { get; set; }
        

        public DbSet<Student>? Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<Teacher>().ToTable("Teachers");
            modelBuilder.Entity<Student>().ToTable("Students");

            modelBuilder.Entity<Admin>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Admin>(a => a.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Teacher>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Teacher>(a => a.Id).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Student>()
                   .HasOne<User>()
                   .WithOne()
                   .HasForeignKey<Student>(a => a.Id).OnDelete(DeleteBehavior.Cascade);
        }





    }
}
