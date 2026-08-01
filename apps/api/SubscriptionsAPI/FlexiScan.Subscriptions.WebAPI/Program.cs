
using FlexiScan.Subscriptions.Data;
using FlexiScan.Subscriptions.Data.Models;
using FlexiScan.Subscriptions.Services.Data.Implementations;
using FlexiScan.Subscriptions.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace FlexiScan.Subscriptions.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddFlexiScanJwtAuth();

            var gatewayUrl = builder.Configuration.GetValue<string>("GatewayUrl");
            if (string.IsNullOrEmpty(gatewayUrl))
            {
                throw new Exception("GatewayUrl is not configured in appsettings or environment variables.");
            }

            var strictCorsPolicy = "_strictCorsPolicy";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: strictCorsPolicy,
                                  policy =>
                                  {
                                      policy.WithOrigins(gatewayUrl)
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowCredentials();
                                  });
            });

            string? connectionString = builder.Configuration.GetConnectionString("SubscriptionsDb");
            builder.Services.AddDbContext<SubscriptionsDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped<ISubscriptionPlanService, SubscriptionPlanService>();

            var stripeSecretKey = builder.Configuration.GetValue<String>("Stripe:SecretKey");
            StripeConfiguration.ApiKey = stripeSecretKey;

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddSwaggerGen();
            }

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<SubscriptionsDbContext>();
                db.Database.Migrate();

                var subscriptionService = scope.ServiceProvider.GetRequiredService<ISubscriptionPlanService>();
                await subscriptionService.UpdateSubscriptionPlans();
            }

            app.UseCors(strictCorsPolicy);

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}
