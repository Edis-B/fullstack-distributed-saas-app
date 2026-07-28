using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace FlexiScan.Users.Web.Infrastructure.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection RegisterPrivateRSAKeyService(this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment environment)
        {
            var privateKeyRelativePath = configuration.GetValue<string>("JwtSettings:PrivateKeyPath");
            if (string.IsNullOrEmpty(privateKeyRelativePath))
            {
                throw new Exception("PrivateKeyPath is not configured in appsettings or environment variables.");
            }

            var privateKeyFullPath = Path.Combine(environment.ContentRootPath, privateKeyRelativePath);

            var privateKeyPem = File.ReadAllText(privateKeyFullPath);
            RSA privateRSA = RSA.Create();
            
            privateRSA.ImportFromPem(privateKeyPem);
            RsaSecurityKey privateSecurityKey = new RsaSecurityKey(privateRSA);

            services.AddSingleton(privateSecurityKey);

            return services;
        }
    }
}
