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
        public async Task UpdateSubscriptionPlans()
        {
            var priceService = new PriceService();
            var options = new PriceListOptions { Active = true, Expand = new List<string> { "data.product" } };
            StripeList<Price> stripePrices = await priceService.ListAsync(options);

            // Tracks plans in memory
            var trackedPlans = new Dictionary<string, SubscriptionPlan>();

            foreach (var stripePrice in stripePrices)
            {
                if (!trackedPlans.TryGetValue(stripePrice.ProductId, out var subscriptionPlan))
                {
                    subscriptionPlan = await _dbContext.SubscriptionPlans
                        .FirstOrDefaultAsync(p => p.StripeProductId == stripePrice.ProductId);

                    var extractedFeatures = ExtractPlanFeatures(stripePrice.Product.Metadata);

                    if (subscriptionPlan == null)
                    {
                        subscriptionPlan = new SubscriptionPlan
                        {
                            Name = stripePrice.Product.Name ?? "Unknown Product Name",
                            StripeProductId = stripePrice.ProductId,
                            Features = extractedFeatures
                        };

                        _dbContext.SubscriptionPlans.Add(subscriptionPlan);
                    }
                    else
                    {
                        subscriptionPlan.Name = stripePrice.Product.Name ?? "Unknown Product Name";
                        subscriptionPlan.Features = extractedFeatures;
                    }

                    trackedPlans[stripePrice.ProductId] = subscriptionPlan;
                }

                var subscriptionPrice = await _dbContext.SubscriptionPrices
                    .FirstOrDefaultAsync(p => p.StripePriceId == stripePrice.Id);

                if (subscriptionPrice == null)
                {
                    subscriptionPrice = new SubscriptionPrice
                    {
                        StripePriceId = stripePrice.Id,
                        Price = (decimal)stripePrice.UnitAmount! / 100m,
                        SubscriptionPlan = subscriptionPlan,
                        Name = stripePrice.Nickname ?? "Unknown Pricing Plan Name",
                        Currency = stripePrice.Currency,
                        Interval = stripePrice.Recurring?.Interval ?? "one-time"
                    };

                    _dbContext.SubscriptionPrices.Add(subscriptionPrice);
                }
                else
                {
                    subscriptionPrice.Price = (decimal)stripePrice.UnitAmount! / 100m;
                    subscriptionPrice.Name = stripePrice.Nickname ?? "Unknown Pricing Plan Name";
                    subscriptionPrice.Currency = stripePrice.Currency;
                    subscriptionPrice.Interval = stripePrice.Recurring?.Interval ?? "one-time";
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        private PlanFeatures ExtractPlanFeatures(Dictionary<string, string> metadata)
        {
            metadata ??= new Dictionary<string, string>();

            return new PlanFeatures
            {
                MaxActiveCodes = metadata.TryGetValue("max_active_codes", out var macStr)
                    && int.TryParse(macStr, out var mac) ? mac : 1,

                MaxDailyScans = metadata.TryGetValue("max_daily_scans", out var mdsStr)
                    && int.TryParse(mdsStr, out var mms) ? mms : 100,

                HasAdvancedAnalytics = metadata.TryGetValue("has_advanced_analytics", out var haaStr)
                    && bool.TryParse(haaStr, out var haa) && haa,

                AllowCustomLogos = metadata.TryGetValue("allow_custom_logos", out var aclStr)
                    && bool.TryParse(aclStr, out var acl) && acl,

                AllowTrackingPixels = metadata.TryGetValue("allow_tracking_pixels", out var atpStr)
                    && bool.TryParse(atpStr, out var atp) && atp,

                AllowCustomDomains = metadata.TryGetValue("allow_custom_domains", out var acdStr)
                    && bool.TryParse(acdStr, out var acd) && acd,

                AllowApiAccess = metadata.TryGetValue("allow_api_access", out var aaaStr)
                    && bool.TryParse(aaaStr, out var aaa) && aaa
            };
        }
    }
}
