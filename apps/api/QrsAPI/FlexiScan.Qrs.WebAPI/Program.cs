
using FlexiScan.Qrs.Data;
using FlexiScan.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FlexiScan.Qrs.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddFlexiScanGatewayCors(builder.Configuration);

            string? connectionString = builder.Configuration.GetConnectionString("QrsConnection");
            if (connectionString == null)
            {
                throw new Exception("QrsConnection was not found");
            }

            builder.Services.AddDbContext<QrsDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddControllers();
            builder.Services.AddFlexiScanJwtAuth();

            builder.Services.AddOpenApi();

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

            app.UseCors(CorsPolicyExtensions.GatewayCorsPolicyName);
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
