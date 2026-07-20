
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

            var app = builder.Build();

            // Put the YARP middleware in the pipeline
            app.MapReverseProxy();

            app.Run();
        }
    }
}
