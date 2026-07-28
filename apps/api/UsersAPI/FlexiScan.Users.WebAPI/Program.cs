
using FlexiScan.Users.Data;
using FlexiScan.Users.Data.Models;
using FlexiScan.Users.Services.Data.Implementations;
using FlexiScan.Users.Services.Data.Interfaces;
using FlexiScan.Users.Web.Infrastructure.Extensions;
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

            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.RegisterPrivateRSAKeyService(
                builder.Configuration,
                builder.Environment);

            builder.Services.AddFlexiScanJwtAuth();

            builder.Services.AddControllers();

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddSwaggerGen();
            }

            builder.Services.AddDbContext<UsersDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("UsersDb")));

            builder.Services.AddAuthorization();

            builder.Services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<UsersDbContext>();

            var app = builder.Build();

            app.UseCors(strictCorsPolicy);

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
                if (db == null) throw new Exception("Error creating a db context instance!");
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
