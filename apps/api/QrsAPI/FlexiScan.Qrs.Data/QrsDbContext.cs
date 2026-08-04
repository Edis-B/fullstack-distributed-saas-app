using FlexiScan.Qrs.Data.Models;
using FlexiScan.Qrs.Services.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexiScan.Qrs.Data
{
    public class QrsDbContext : DbContext
    {
        public QrsDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomDomain>()
                .HasIndex(c => c.DomainName)
                .IsUnique();
        }

        public virtual DbSet<CustomDomain> CustomDomains { get; set; }
        public virtual DbSet<QrCode> QrCodes { get; set; }
        public virtual DbSet<ScanEvent> ScanEvents { get; set; }
        public virtual DbSet<UserUsageCache> UserUsageCaches { get; set; }
    }
}
