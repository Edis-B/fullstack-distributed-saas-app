using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace FlexiScan.Shared.Extensions
{
    public static class CorsPolicyExtensions
    {
        public const string GatewayCorsPolicyName = "_strictCorsPolicy";

        public static IServiceCollection AddFlexiScanGatewayCors(this IServiceCollection services, IConfiguration configuration)
        {
            var gatewayUrl = configuration.GetValue<string>("GatewayUrl");
            if (gatewayUrl == null)
            {
                throw new Exception("Gateway Url was not found in app settings");
            }

            services.AddCors(options =>
            {
                options.AddPolicy(name: GatewayCorsPolicyName,
                                  policy =>
                                  {
                                      policy.WithOrigins(gatewayUrl)
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowCredentials();
                                  });
            });

            return services;
        }
    }
}
