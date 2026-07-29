using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlexiScan.Subscriptions.Data.Models
{
    public class UserSubscription
    {
        [Key]
        public int Id { get; set; }
        public string StripeSubscriptionId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public DateTime CurrentPeriodStart { get; set; }
        public DateTime CurrentPeriodEnd { get; set; }
        public bool CancelAtPeriodEnd { get; set; }

        public int BillingCustomerId { get; set; }
        [ForeignKey(nameof(BillingCustomerId))]
        public BillingCustomer Customer { get; set; } = null!;

        public int SubscriptionPriceId { get; set; }
        [ForeignKey(nameof(SubscriptionPriceId))]
        public SubscriptionPrice SubscriptionPrice { get; set; } = null!;
    }
}
