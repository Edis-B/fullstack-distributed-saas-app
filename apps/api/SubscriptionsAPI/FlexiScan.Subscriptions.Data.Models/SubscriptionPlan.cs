using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlexiScan.Subscriptions.Data.Models
{
    public class SubscriptionPlan
    {
        [Key]
        public int Id { get; set; }
        public string StripeProductId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public ICollection<SubscriptionPrice> SubscriptionPrices { get; set; } = new List<SubscriptionPrice>();
        public PlanFeatures Features { get; set; } = new PlanFeatures();
    }
}
