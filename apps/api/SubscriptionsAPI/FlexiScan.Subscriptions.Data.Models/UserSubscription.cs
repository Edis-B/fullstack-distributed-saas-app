using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlexiScan.Subscriptions.Data.Models
{
    public class UserSubscription
    {
        public int Id { get; set; }
        public string StripeSubscriptionId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CurrentPeriodEnd { get; set; }
        public bool CancelAtPeriodEnd { get; set; }

        public int BillingCustomerId { get; set; }
        [ForeignKey(nameof(BillingCustomerId))]
        public BillingCustomer Customer { get; set; } = null!;

        public int SubscriptionPlanId { get; set; }
        [ForeignKey(nameof(SubscriptionPlanId))]
        public SubscriptionPlan Plan { get; set; } = null!;
    }
}
