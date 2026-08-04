using FlexiScan.Subscriptions.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FlexiScan.Subscriptions.Data
{
    public class SubscriptionsDbContext : DbContext
    {
        public SubscriptionsDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // flatten its properties into columns on the SubscriptionPlans table
            modelBuilder.Entity<SubscriptionPlan>()
                .OwnsOne(p => p.Features);
        }

        public virtual DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public virtual DbSet<SubscriptionPrice> SubscriptionPrices { get; set; }
        public virtual DbSet<UserSubscription> UserSubscriptions { get; set; }
        public virtual DbSet<BillingCustomer> BillingCustomers { get; set; }
    }
}
