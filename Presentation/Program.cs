using Infrastructure.Commons;
using Infrastructure.Repositories;
using Domain.Entities;
using Serilog;
using Serilog.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Application.Implementations.Services;
using Domain.Commons.Enums;
using Infrastructure.Contexts;
using Application.Interfaces.Repositories;

namespace FakeIMDB
{
    public class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new();
            var logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.Seq("http://localhost:5341/")
                .CreateLogger();

            var msLogger = new SerilogLoggerFactory(logger).CreateLogger<OMDBMovieRepository>();
            var movieCacheLogger = new SerilogLoggerFactory(logger).CreateLogger<MovieService>();
            var movieCacheContext = new MovieCacheContext();
            var movieDatabaseRepository = new MovieDatabaseRepository(movieCacheContext);
            Serilog.Debugging.SelfLog.Enable(Console.Error);
            logger.Information("Hello, {Name}!", Environment.UserName);

            OMDBMovieRepository movieRepository = new OMDBMovieRepository(client, "c340b01e", msLogger);
            MovieService movieService = new MovieService(movieDatabaseRepository, movieRepository, movieCacheLogger);

            while (true)
            {
                Console.WriteLine("Siemaneczko, wybierz co chcialbys zrobic: ");
                Console.WriteLine("1. Wyswietlic informacje o filmie po ID.");
                Console.WriteLine("2. Wyswietlic informacje o filmie po tytule.");
                Console.WriteLine("3. Wyszukac film po nazwie.");
                Console.WriteLine("4. Debug.");
                string key = Console.ReadLine();

                if (key == "1")
                {
                    Console.WriteLine("Podaj ID filmu: ");
                    string movieID = Console.ReadLine();
                    Console.Clear();
                    if (!string.IsNullOrEmpty(movieID))
                    {
                        MovieInfo newMovie = movieService.GetMovieByID(movieID).Result;
                        if (newMovie != null)
                        {
                            Console.WriteLine(newMovie.ToString());
                        }
                    }
                }
                else if (key == "2")
                {
                    Console.WriteLine("Podaj tytul filmu: ");
                    string movieTitle = Console.ReadLine();
                    Console.Clear();
                    if (!string.IsNullOrEmpty(movieTitle))
                    {
                        MovieInfo newMovie = movieService.GetMovieByTitle(movieTitle).Result;
                        if (newMovie != null)
                        {
                            Console.WriteLine(newMovie.ToString());
                        }
                    }
                }
                else if (key == "3")
                {
                    Console.WriteLine("Podaj tytul filmu: ");
                    string movieTitle = Console.ReadLine();
                    Console.Clear();
                    if (!string.IsNullOrEmpty(movieTitle))
                    {
                        MovieList newList = movieService.GetMovieListByTitle(movieTitle).Result;
                        if (newList != null)
                        {
                            Console.WriteLine(newList.ToString());
                        }
                    }
                }
                else if (key == "4")
                {

                }
                Console.ReadKey();
                Console.Clear();
                logger.Dispose();
            }

        }
    }
}