using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FlexiScan.Subscriptions.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlexiScan.Subscriptions.Data.Models
{
    public class BillingCustomer
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty; 
        public string StripeCustomerId { get; set; } = string.Empty;
        public ICollection<UserSubscription> Subscriptions { get; set; } = new List<UserSubscription>();
    }
}
