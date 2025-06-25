using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenditure.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        { 
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Categories
            modelBuilder.Entity<Category>().Property(c => c.Title).HasColumnType("nvarchar(100)");
            modelBuilder.Entity<Category>().Property(c => c.Type).HasColumnType("nvarchar(100)");
            modelBuilder.Entity<Category>().Property(c => c.Icon).HasColumnType("nvarchar(100)");

            //Transactions
            modelBuilder.Entity<Transaction>().Property(c => c.Note).HasColumnType("nvarchar(100)");
        }
    }
}
