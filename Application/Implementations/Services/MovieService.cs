using Application.Interfaces.Services;
using Application.Interfaces.Repositories;
using Domain.Commons.Enums;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Implementations.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieCache movieCache;
        private readonly IMovieRepository movieRepository;
        private readonly ILogger<MovieService> serviceLogger;

        public MovieService(IMovieCache movieCache, IMovieRepository movieRepository, ILogger<MovieService> serviceLogger)
        {
            this.movieCache = movieCache;
            this.movieRepository = movieRepository;
            this.serviceLogger = serviceLogger;
        }

        public Task<MovieInfo> GetMovieByID(string id, TypeOptions? type = null, int? year = null, PlotOptions plot = PlotOptions.Short)
        {
            var allMovies = movieCache.GetAllMovies()
                .Where(x => x.Query == id)
                .Where(x => x.MediaType == type)
                .Where(x => x.Year == year)
                .Where(x => x.PlotOption == plot)
                .FirstOrDefault();

            if (allMovies != null)
            {
                serviceLogger.LogDebug("Znaleziono informacje o filmie w pamieci cache!");
                return Task.FromResult(allMovies.MovieInfo);
            }
            else
            {
                serviceLogger.LogDebug("Nie znaleziono informacji o filmie w pamieci cache, pobieram z API...");
                MovieInfo newMovie = movieRepository.GetMovieByID(id, type, year, plot).Result;
                MovieInfoCache newMovieInfoCache = new(newMovie, id, type, year, plot);
                _ = movieCache.AddMovieToDatabaseAsync(newMovieInfoCache);
                return Task.FromResult(newMovie);
            }
        }

        public Task<MovieInfo> GetMovieByTitle(string title, TypeOptions? type = null, int? year = null, PlotOptions plot = PlotOptions.Short)
        {
            var allMovies = movieCache.GetAllMovies()
                .Where(x => x.Query == title)
                .Where(x => x.MediaType == type)
                .Where(x => x.Year == year)
                .Where(x => x.PlotOption == plot)
                .FirstOrDefault();

            if (allMovies != null)
            {
                serviceLogger.LogDebug("Znaleziono informacje o filmie w pamieci cache!");
                return Task.FromResult(allMovies.MovieInfo);
            }
            else
            {
                serviceLogger.LogDebug("Nie znaleziono informacji o filmie w pamieci cache, pobieram z API...");
                MovieInfo newMovie = movieRepository.GetMovieByTitle(title, type, year, plot).Result;
                MovieInfoCache newMovieInfoCache = new(newMovie, title, type, year, plot);
                _ = movieCache.AddMovieToDatabaseAsync(newMovieInfoCache);
                return Task.FromResult(newMovie);
            }
        }

        public Task<MovieList> GetMovieListByTitle(string title, TypeOptions? type = null, int? year = null)
        {
            var allMovies = movieCache.GetAllMoviesLists()
                .Where(x => x.QueryTitle == title)
                .Where(x => x.MediaType == type)
                .Where(x => x.Year == year)
                .FirstOrDefault();

            if (allMovies != null)
            {
                serviceLogger.LogDebug("Znaleziono informacje o filmie w pamieci cache!");
                return Task.FromResult(allMovies.MovieList);
            }
            else
            {
                serviceLogger.LogDebug("Nie znaleziono informacji o filmie w pamieci cache, pobieram z API...");
                MovieList newList = movieRepository.GetMovieListByTitle(title, type, year).Result;
                MovieListCache newMoveListCache = new(newList, title, type, year);
                _ = movieCache.AddMovieListToDatabaseAsync(newMoveListCache);
                return Task.FromResult(newList);
            }
        }
    }
}
