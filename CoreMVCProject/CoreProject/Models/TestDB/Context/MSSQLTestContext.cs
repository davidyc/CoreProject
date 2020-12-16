using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Models.TestDB.Context
{
    public class MSSQLTestContext : DbContext
    {
        public DbSet<TestModel> TestModels { get; set; }
        public MSSQLTestContext(DbContextOptions<MSSQLTestContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
