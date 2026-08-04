
using FlexiScan.Qrs.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexiScan.Qrs.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("QrsConnection");
            if (connectionString == null)
            {
                throw new Exception("QrsConnection was not found");
            }

            builder.Services.AddDbContext<QrsDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddControllers();
            builder.Services.AddFlexiScanJwtAuth();

            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
