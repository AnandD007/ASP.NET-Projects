global using Microsoft.EntityFrameworkCore;
using CustomerWebApp.Services.Data.Entities;
// using System.Data.Entity;

namespace CustomerWebApp.Services.Data.DBContext
{
    public class CustomerDBContext : DbContext
    {
        public CustomerDBContext(DbContextOptions option) : base(option) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                        .HasKey(c => c.CustomerId)
                        .HasName("PrimaryKey_Id");
        }

        public DbSet<Customer> Customer { get; set; }

    }
}
