using CoreProject.Models.AppModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Models
{
    public class AppDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "davidyc";
            string userRoleName = "user";

            //string adminEmail = "davydovsergey.sd@gmail.com";
            //string adminPassword = "123456";


            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });


            //User adminUser = new User { Id = 1, Login = "davidyc", Email = adminEmail, Password = adminPassword };
            //adminRole.Users.Add(adminUser);
            //adminUser.Roles.Add(adminRole);
            //modelBuilder.Entity<User>().HasData(new User[] { adminUser });

            base.OnModelCreating(modelBuilder);
        }
    }
}
