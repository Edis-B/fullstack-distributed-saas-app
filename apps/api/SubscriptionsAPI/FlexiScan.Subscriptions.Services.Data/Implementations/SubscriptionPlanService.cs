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

            var options = new PriceListOptions { Active = true, Expand = new List<string> { "data.product" } };
            StripeList<Price> stripePrices = await priceService.ListAsync(options);
            foreach (var stripePrice in stripePrices)
            {
                var subscriptionPriceExists = await _dbContext.SubscriptionPrices
                    .AnyAsync(p => p.StripePriceId == stripePrice.Id);

                if (subscriptionPriceExists) continue;

                var subscriptionPlan = await _dbContext.SubscriptionPlans
                    .FirstOrDefaultAsync(p => p.StripeProductId == stripePrice.ProductId);

                var subscriptionPlanExists = subscriptionPlan != null;
                if (!subscriptionPlanExists)
                {
                    subscriptionPlan = new SubscriptionPlan
                    {
                        Name = stripePrice.Product.Name ?? "Unknown Product Name",
                        StripeProductId = stripePrice.ProductId,
                    };
                }

                var newPrice = new SubscriptionPrice
                {
                    StripePriceId = stripePrice.Id,
                    Price = (decimal)stripePrice.UnitAmount! / 100m,
                    SubscriptionPlan = subscriptionPlan!,
                    Name = stripePrice.Nickname ?? "Unknown Pricing Plan Name",
                    Currency = stripePrice.Currency,
                    Interval = stripePrice.Recurring?.Interval ?? "one-time"
                };

                if (!subscriptionPlanExists)
                    _dbContext.SubscriptionPlans.Add(subscriptionPlan!);

                _dbContext.SubscriptionPrices.Add(newPrice);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
