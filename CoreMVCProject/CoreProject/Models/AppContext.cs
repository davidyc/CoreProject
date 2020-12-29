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
        public DbSet<MyProject> MyProjects { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string davidycRolename = "davidyc";
            string adminRoleName = "admin";
            string userRoleName = "user";

            Role davidycRole = new Role { Id = 1, Name = davidycRolename };
            Role adminRole = new Role { Id = 2, Name = adminRoleName };
            Role userRole = new Role { Id = 3, Name = userRoleName };
            modelBuilder.Entity<Role>().HasData(new Role[] { davidycRole, adminRole, userRole });
          

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CoreProject.Models.AppModel.MyProject> MyProject { get; set; }
    }
}
