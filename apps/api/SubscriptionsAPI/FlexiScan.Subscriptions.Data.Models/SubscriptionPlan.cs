using System.ComponentModel.DataAnnotations.Schema;

namespace FlexiScan.Subscriptions.Data.Models
{
    public class SubscriptionPlan
    {
        public int Id { get; set; }
        public string StripePriceId { get; set; } = string.Empty;
        public string StripeProductId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Interval { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string Currency { get; set; } = string.Empty;

        public ICollection<UserSubscription> Subscriptions { get; set; } = new List<UserSubscription>();
    }
}
