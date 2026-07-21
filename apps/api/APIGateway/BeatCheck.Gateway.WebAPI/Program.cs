
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

            var app = builder.Build();

            app.UseCors(strictCorsPolicy);

            // Put the YARP middleware in the pipeline
            app.MapReverseProxy();

            app.Run();
        }
    }
}
