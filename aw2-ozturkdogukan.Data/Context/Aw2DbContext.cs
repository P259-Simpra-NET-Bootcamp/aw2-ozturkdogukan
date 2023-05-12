using aw2_ozturkdogukan.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aw2_ozturkdogukan.Data.Context
{
    public class Aw2DbContext : DbContext
    {
        public Aw2DbContext(DbContextOptions<Aw2DbContext> options) : base(options)
        {

        }

        public DbSet<Staff> Staff { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
