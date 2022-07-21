using Application.Implementations.Services;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Infrastructure.Common;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Presentation
{
    public class Program
    {
        private static void AddServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHostedService<ConsoleUIService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IMovieRepository, OMDBMovieRepository>();
            services.AddScoped<IMovieCache, MovieDatabaseRepository>();
            services.AddDbContext<MovieCacheContext>();

            services.Configure<APISettings>(context.Configuration.GetRequiredSection("APISettings"));
            services.Configure<DatabaseSettings>(context.Configuration.GetRequiredSection("DatabaseSettings"));
        }

        static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) => config
                    .AddJsonFile("appsettings.json")
                    .AddUserSecrets<Program>(true))
                .ConfigureServices(AddServices)
                .UseSerilog((context, loggerConfig) => loggerConfig
                    .WriteTo.Console()
                    .WriteTo.Seq("http://localhost:5341/")
                    .MinimumLevel.Verbose())
                .Build();

            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Hello, {Name}!", Environment.UserName);

            await host.RunAsync();
        }
    }
}