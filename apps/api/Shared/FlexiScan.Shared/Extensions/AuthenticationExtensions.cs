using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddFlexiScanJwtAuth(this IServiceCollection services, string publicKeyPath = "Keys/public.pem")
    {
        var publicKeyPem = File.ReadAllText(publicKeyPath);
        RSA publicRSA = RSA.Create();
        publicRSA.ImportFromPem(publicKeyPem);
        RsaSecurityKey publicSecurityKey = new RsaSecurityKey(publicRSA);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var token = context.Request.Cookies["jwt_token"];
                        if (!string.IsNullOrEmpty(token))
                        {
                            context.Token = token;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorization();

        return services;
    }
}