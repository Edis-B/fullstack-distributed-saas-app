using FlexiScan.Subscriptions.Data;
using FlexiScan.Subscriptions.Data.Models;
using FlexiScan.Subscriptions.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexiScan.Subscriptions.Services.Data.Implementations
{
    public class SubscriptionPlanService : ISubscriptionPlanService
    {
        private readonly SubscriptionsDbContext _dbContext;
        public SubscriptionPlanService(SubscriptionsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task DiscoverSubscriptionPlans()
        {
            var priceService = new PriceService();

            var options = new PriceListOptions { Active = true };
            StripeList<Price> stripePrices = await priceService.ListAsync(options);

            foreach (var stripePrice in stripePrices)
            {
                var exists = await _dbContext.SubscriptionPlans
                    .AnyAsync(p => p.StripePriceId == stripePrice.Id);

                if (!exists)
                {
                    var newPlan = new SubscriptionPlan
                    {
                        StripePriceId = stripePrice.Id,
                        StripeProductId = stripePrice.ProductId,
                        Price = (decimal)stripePrice.UnitAmount! / 100m,
                        Currency = stripePrice.Currency,
                        Interval = stripePrice.Recurring?.Interval ?? "one-time"
                    };

                    _dbContext.SubscriptionPlans.Add(newPlan);
                }
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
