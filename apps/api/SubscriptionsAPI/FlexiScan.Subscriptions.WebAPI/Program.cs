
using Flexiscan.Subscriptions.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexiScan.Subscriptions.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddFlexiScanJwtAuth();

            builder.Services.AddDbContext<SubscriptionsDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SubscriptionsDb")));

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
                var db = scope.ServiceProvider.GetService<SubscriptionsDbContext>();
                db.Database.Migrate();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
