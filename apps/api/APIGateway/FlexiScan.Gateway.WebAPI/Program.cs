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

            // Add YARP services to the container and load the config section
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

            var publicKeyPem = File.ReadAllText("Keys/public.pem");
            RSA publicRSA = RSA.Create();
            publicRSA.ImportFromPem(publicKeyPem);
            RsaSecurityKey publicSecurityKey = new RsaSecurityKey(publicRSA);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "FlexiScan.UsersAPI",

                        ValidateAudience = true,
                        ValidAudience = "FlexiScan.Frontend",

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = publicSecurityKey
                    };
                });

            builder.Services.AddAuthorization();

            var app = builder.Build();

            app.UseCors(strictCorsPolicy);

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapReverseProxy();

            app.Run();
        }
    }
}
