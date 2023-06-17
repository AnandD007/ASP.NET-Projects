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
                       .HasKey(m => m.MatterId)
                       .HasName("PrimaryKey_MatterId");

            modelBuilder.Entity<Jurisdiction>()
                        .HasKey(j => j.JurisdictionId)
                        .HasName("PrimaryKey_JurisdictionId");

            modelBuilder.Entity<Client>()
                        .HasKey(c => c.ClientId)
                        .HasName("PrimaryKey_ClientId");

            modelBuilder.Entity<Attorney>()
                        .HasKey(a => a.AttorneyId)
                        .HasName("PrimaryKey_AttorneyId");

            modelBuilder.Entity<Invoice>()
                        .HasKey(i => i.InvoiceId)
                        .HasName("PrimaryKey_InvoiceId");

            modelBuilder.Entity<Jurisdiction>()
                        .HasMany(e => e.Attorneys)
                        .WithOne(e => e.Jurisdiction)
                        .HasForeignKey(e => e.JurisdictionId)
                        .IsRequired(false);

            modelBuilder.Entity<Jurisdiction>()
                        .HasMany(e => e.Matters)
                        .WithOne(e => e.Jurisdiction)
                        .HasForeignKey(e => e.JurisdictionId)
                        .IsRequired(false);

            modelBuilder.Entity<Client>()
                        .HasMany(e => e.Matters)
                        .WithOne(e => e.Client)
                        .HasForeignKey(e => e.ClientId)
                        .IsRequired(false);

            modelBuilder.Entity<Attorney>()
                        .HasMany(e => e.BillingAttorneyMatters)
                        .WithOne(e => e.BillingAttorney)
                        .HasForeignKey(e => e.BillingAttorneyId)
                        .IsRequired(false);

            modelBuilder.Entity<Attorney>()
                        .HasMany(e => e.ResponsibleAttorneyMatters)
                        .WithOne(e => e.ResponsibleAttorney)
                        .HasForeignKey(e => e.ResponsibleAttorneyId)
                        .IsRequired(false);

            modelBuilder.Entity<Attorney>()
                        .HasMany(e => e.Invoices)
                        .WithOne(e => e.Attorney)
                        .HasForeignKey(e => e.AttorneyId)
                        .IsRequired(false);

            modelBuilder.Entity<Matter>()
                        .HasMany(e => e.Invoices)
                        .WithOne(e => e.Matter)
                        .HasForeignKey(e => e.MatterId)
                        .IsRequired(false);

        }
    }

}

