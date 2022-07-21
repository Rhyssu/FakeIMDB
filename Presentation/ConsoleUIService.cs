using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.Extensions.Hosting;

namespace Presentation
{
    internal class ConsoleUIService : BackgroundService
    {
        private readonly IMovieService movieService;

        public ConsoleUIService(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
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
                        MovieInfo newMovie = await movieService.GetMovieByID(movieID, cancellationToken: stoppingToken);
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
                        MovieInfo newMovie = await movieService.GetMovieByTitle(movieTitle, cancellationToken: stoppingToken);
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
                        MovieList newList = await movieService.GetMovieListByTitle(movieTitle, cancellationToken: stoppingToken);
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
            }
        }
    }
}
