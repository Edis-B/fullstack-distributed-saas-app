
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace BeatCheck.Gateway.WebAPI
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
                                            .AllowAnyMethod();
                                  });
            });

            var publicKeyPem = File.ReadAllText("Keys/public.pem");
            using RSA rsa = RSA.Create();
            rsa.ImportFromPem(publicKeyPem);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "BeatCheck.UsersAPI",

                        ValidateAudience = true,
                        ValidAudience = "BeatCheck.Frontend",

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new RsaSecurityKey(rsa)
                    };
                });

            var app = builder.Build();

            app.UseCors(strictCorsPolicy);

            // Put the YARP middleware in the pipeline
            app.MapReverseProxy();

            app.Run();
        }
    }
}
