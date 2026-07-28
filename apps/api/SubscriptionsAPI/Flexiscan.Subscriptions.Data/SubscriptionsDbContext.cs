using FlexiScan.Subscriptions.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FlexiScan.Subscriptions.Data
{
    public class SubscriptionsDbContext : DbContext
    {
        public SubscriptionsDbContext(DbContextOptions options) : base(options)
        {
        }

        protected SubscriptionsDbContext()
        {
        }

        public virtual DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public virtual DbSet<UserSubscription> UserSubscriptions { get; set; }
        public virtual DbSet<BillingCustomer> BillingCustomers { get; set; }
    }
}
