global using Microsoft.EntityFrameworkCore;
using MatterManagementWebApp.Services.Models.Entities;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

// using System.Data.Entity;

namespace MatterManagementWebApp.Services.Data.DBContext
{
    public class MatterDBContext : DbContext
    {
        public MatterDBContext(DbContextOptions<MatterDBContext> options) : base(options)
        {
        }
        public DbSet<Matter>? Matters { get; set; }
        public DbSet<Invoice>? Invoices { get; set; }
        public DbSet<Client>? Clients { get; set; }
        public DbSet<Jurisdiction>? Jurisdictions { get; set; }
        public DbSet<Attorney>? Attorneys { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Matter>()
              .HasOne(m => m.BillingAttorney)
              .WithMany()
              .HasForeignKey(m => m.BillingAttorneyId)
              .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Matter>()
                .HasOne(m => m.ResponsibleAttorney)
                .WithMany()
                .HasForeignKey(m => m.ResponsibleAttorneyId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Matter>()
               .HasOne(m => m.ResponsibleAttorney)
               .WithMany()
               .HasForeignKey(m => m.ResponsibleAttorneyId)
               .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Matter>()
                .HasOne(m => m.Client)
                .WithMany(c => c.Matters)
                .HasForeignKey(m => m.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Attorney>()
                .HasOne(a => a.Jurisdiction)
                .WithMany(j => j.Attorneys)
                .HasForeignKey(a => a.JurisdictionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Matter>()
                .HasOne(m => m.Jurisdiction)
                .WithMany(j => j.Matters)
                .HasForeignKey(m => m.JurisdictionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Matter)
                .WithMany(m => m.Invoices)
                .HasForeignKey(i => i.MatterId)
            .OnDelete(DeleteBehavior.NoAction);

        }
    }

}

