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

        public async Task<MovieInfo> GetMovieByID(string id, TypeOptions? type = null, int? year = null, PlotOptions plot = PlotOptions.Short)
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
                return allMovies.MovieInfo;
            }
            else
            {
                serviceLogger.LogDebug("Nie znaleziono informacji o filmie w pamieci cache, pobieram z API...");
                MovieInfo newMovie = await movieRepository.GetMovieByID(id, type, year, plot);
                MovieInfoCache newMovieInfoCache = new(newMovie, id, type, year, plot);
                _ = movieCache.AddMovieToDatabaseAsync(newMovieInfoCache);
                return newMovie;
            }
        }

        public async Task<MovieInfo> GetMovieByTitle(string title, TypeOptions? type = null, int? year = null, PlotOptions plot = PlotOptions.Short)
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
                return allMovies.MovieInfo;
            }
            else
            {
                serviceLogger.LogDebug("Nie znaleziono informacji o filmie w pamieci cache, pobieram z API...");
                MovieInfo newMovie = await movieRepository.GetMovieByTitle(title, type, year, plot);
                MovieInfoCache newMovieInfoCache = new(newMovie, title, type, year, plot);
                _ = movieCache.AddMovieToDatabaseAsync(newMovieInfoCache);
                return newMovie;
            }
        }

        public async Task<MovieList> GetMovieListByTitle(string title, TypeOptions? type = null, int? year = null)
        {
            var allMovies = movieCache.GetAllMoviesLists()
                .Where(x => x.QueryTitle == title)
                .Where(x => x.MediaType == type)
                .Where(x => x.Year == year)
                .FirstOrDefault();

            if (allMovies != null)
            {
                serviceLogger.LogDebug("Znaleziono informacje o filmie w pamieci cache!");
                return allMovies.MovieList;
            }
            else
            {
                serviceLogger.LogDebug("Nie znaleziono informacji o filmie w pamieci cache, pobieram z API...");
                MovieList newList = await movieRepository.GetMovieListByTitle(title, type, year);
                MovieListCache newMoveListCache = new(newList, title, type, year);
                await movieCache.AddMovieListToDatabaseAsync(newMoveListCache);
                return newList;
            }
        }
    }
}
