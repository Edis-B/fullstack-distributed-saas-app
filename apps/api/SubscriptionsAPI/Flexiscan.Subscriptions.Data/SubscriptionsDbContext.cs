using Microsoft.EntityFrameworkCore;

namespace Flexiscan.Subscriptions.Data
{
    public class SubscriptionsDbContext : DbContext
    {
        public SubscriptionsDbContext(DbContextOptions options) : base(options)
        {
        }

        protected SubscriptionsDbContext()
        {
        }
    }
}
