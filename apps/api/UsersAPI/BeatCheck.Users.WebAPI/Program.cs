
using BeatCheck.Users.Data.Models;
using BeatCheck.Users.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BeatCheck.Users.WebAPI
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
                                            .AllowAnyMethod();
                                  });
            });

            builder.Services.AddControllers();

            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<UsersDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddAuthorization();

            builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
                .AddEntityFrameworkStores<UsersDbContext>();

            var app = builder.Build();

            app.UseCors(strictCorsPolicy);

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
                db.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
