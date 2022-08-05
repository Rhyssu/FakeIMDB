using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Application.Interfaces.Services;
using Application.Implementations.Services;
using Application.Interfaces.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Contexts;
using Infrastructure.Common;
using Microsoft.Extensions.Configuration;
using Serilog;
using Microsoft.Extensions.Logging;
using FakeIMDB_GUI.ViewModels;
using FakeIMDB_GUI.Helpers;

namespace FakeIMDB_GUI
{
    public partial class App : System.Windows.Application
    {
        private readonly IHost host;

        public App()
        {
            host = Host.CreateDefaultBuilder()
                .UseConsoleLifetime()
                .ConfigureAppConfiguration((context, config) => config
                    .AddJsonFile("appsettings.json")
                    .AddUserSecrets<App>(true))
                .ConfigureServices(ConfigureServices)
                .UseSerilog((context, loggerConfig) => loggerConfig
                    .WriteTo.Console()
                    .WriteTo.Seq("http://localhost:5341/")
                    .MinimumLevel.Verbose())
                .Build();

            DIResolver.Resolver = (type) => host.Services.GetRequiredService(type);

            var logger = host.Services.GetRequiredService<ILogger<App>>();
            logger.LogInformation("Hello, {Name}!", Environment.UserName);
        }
        private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddHttpClient();
            
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IMovieRepository, OMDBMovieRepository>();
            services.AddScoped<IMovieCache, MovieDatabaseRepository>();
            services.AddDbContext<MovieCacheContext>();

            services.Configure<APISettings>(context.Configuration.GetRequiredSection("APISettings"));
            services.Configure<DatabaseSettings>(context.Configuration.GetRequiredSection("DatabaseSettings"));

            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<MainWindow>();
        }
        public void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
        }
    }
}
