using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlexiScan.Subscriptions.Data.Models
{
    public class SubscriptionPrice
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string StripePriceId { get; set; } = string.Empty;
        public string Interval { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string Currency { get; set; } = string.Empty;

        public int SubscriptionPlanId { get; set; }
        [ForeignKey(nameof(SubscriptionPlanId))]
        public SubscriptionPlan SubscriptionPlan { get; set; } = null!;

        public ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();
    }
}
