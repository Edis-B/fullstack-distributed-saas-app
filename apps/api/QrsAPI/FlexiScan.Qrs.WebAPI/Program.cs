
using Amazon.Runtime;
using Amazon.S3;
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

            // 1. Read the settings from appsettings.json
            var storageConfig = builder.Configuration.GetSection("Storage");
            var accessKey = storageConfig["AccessKey"];
            var secretKey = storageConfig["SecretKey"];
            var serviceUrl = storageConfig["ServiceUrl"];

            // 2. Configure the credentials and client settings
            var awsCredentials = new BasicAWSCredentials(accessKey, secretKey);
            var s3Config = new AmazonS3Config
            {
                ServiceURL = serviceUrl,
                ForcePathStyle = true // MANDATORY for MinIO and local emulators
            };

            // 3. Register the client as RedirectAsync Singleton so your services can inject it
            builder.Services.AddSingleton<IAmazonS3>(new AmazonS3Client(awsCredentials, s3Config));

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
