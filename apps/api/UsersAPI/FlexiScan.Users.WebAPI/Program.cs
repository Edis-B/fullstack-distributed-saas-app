
using FlexiScan.Users.Data;
using FlexiScan.Users.Data.Models;
using FlexiScan.Users.Services.Data.Implementations;
using FlexiScan.Users.Services.Data.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace FlexiScan.Users.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var gatewayUrl = builder.Configuration.GetValue<string>("GatewayUrl");
            if (string.IsNullOrEmpty(gatewayUrl))
            {
                throw new Exception("GatewayUrl is not configured in appsettings or environment variables.");
            }

            builder.Services.AddScoped<IUserService, UserService>();

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

            var privateKeyPem = File.ReadAllText("Keys/private.pem");
            RSA privateRSA = RSA.Create();
            privateRSA.ImportFromPem(privateKeyPem);
            RsaSecurityKey privateSecurityKey = new RsaSecurityKey(privateRSA);
            builder.Services.AddSingleton(privateSecurityKey);

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

            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<UsersDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddAuthorization();

            builder.Services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<UsersDbContext>();

            var app = builder.Build();

            app.UseCors(strictCorsPolicy);

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
                db.Database.Migrate();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
