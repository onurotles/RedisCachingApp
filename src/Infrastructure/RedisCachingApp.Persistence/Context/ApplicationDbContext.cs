using Microsoft.EntityFrameworkCore;
using RedisCachingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCachingApp.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Driver> Drivers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
    }
}
