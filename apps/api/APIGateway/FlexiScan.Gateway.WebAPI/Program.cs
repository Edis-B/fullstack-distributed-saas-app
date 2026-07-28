using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace FlexiScan.Gateway.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

            var frontendUrl = builder.Configuration.GetValue<string>("FrontendUrl");
            if (string.IsNullOrEmpty(frontendUrl))
            {
                throw new Exception("FrontendUrl is not configured in appsettings or environment variables.");
            }

            var strictCorsPolicy = "_strictCorsPolicy";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: strictCorsPolicy,
                                  policy =>
                                  {
                                      policy.WithOrigins(frontendUrl)
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowCredentials();
                                  });
            });

            builder.Services.AddFlexiScanJwtAuth();

            var app = builder.Build();

            app.UseCors(strictCorsPolicy);

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapReverseProxy();

            app.Run();
        }
    }
}
