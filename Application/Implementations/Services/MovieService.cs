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

        public async Task<MovieInfo> GetMovieByID(string id,
            TypeOptions? type = null,
            int? year = null,
            PlotOptions plot = PlotOptions.Short,
            CancellationToken cancellationToken = default)
        {
            var allMovies = movieCache.GetAllMovies()
                .Where(x => x.Query == id)
                .Where(x => x.MediaType == type)
                .Where(x => x.Year == year)
                .Where(x => x.PlotOption == plot)
                .Where(x => DateTime.Now <= x.CreationDate.AddDays(1))
                .FirstOrDefault();

            if (allMovies != null)
            {
                serviceLogger.LogDebug("Znaleziono wpis w pamieci cache!");
                if (allMovies.MovieInfo != null)
                {
                    return allMovies.MovieInfo;
                } 
                else
                {
                    serviceLogger.LogDebug("Nie znaleziono informacji o filmie!");
                    return allMovies.MovieInfo;
                }
            }
            else
            {
                serviceLogger.LogDebug("Nie znaleziono informacji o filmie w pamieci cache, pobieram z API...");
                MovieInfo newMovie = await movieRepository.GetMovieByID(id, type, year, plot);
                MovieInfoCache newMovieInfoCache = new(newMovie, id, type, year, plot);
                await movieCache.AddMovieToDatabaseAsync(newMovieInfoCache, cancellationToken);
                return newMovie;
            }
        }

        public async Task<MovieInfo> GetMovieByTitle(string title,
            TypeOptions? type = null,
            int? year = null,
            PlotOptions plot = PlotOptions.Short,
            CancellationToken cancellationToken = default)
        {
            var allMovies = movieCache.GetAllMovies()
                .Where(x => x.Query == title)
                .Where(x => x.MediaType == type)
                .Where(x => x.Year == year)
                .Where(x => x.PlotOption == plot)
                .Where(x => DateTime.Now <= x.CreationDate.AddDays(1))
                .FirstOrDefault();

            if (allMovies != null)
            {
                serviceLogger.LogDebug("Znaleziono wpis w pamieci cache!");
                if (allMovies.MovieInfo != null)
                {
                    return allMovies.MovieInfo;
                }
                else
                {
                    serviceLogger.LogDebug("Nie znaleziono informacji o filmie!");
                    return allMovies.MovieInfo;
                }
            }
            else
            {
                serviceLogger.LogDebug("Nie znaleziono informacji o filmie w pamieci cache, pobieram z API...");
                MovieInfo newMovie = await movieRepository.GetMovieByTitle(title, type, year, plot);
                MovieInfoCache newMovieInfoCache = new(newMovie, title, type, year, plot);
                await movieCache.AddMovieToDatabaseAsync(newMovieInfoCache, cancellationToken);
                return newMovie;
            }
        }

        public async Task<MovieList> GetMovieListByTitle(string title,
            TypeOptions? type = null,
            int? year = null,
            int? page = null,
            CancellationToken cancellationToken = default)
        {
            var query = movieCache.GetAllMoviesLists()
                .Where(x => x.QueryTitle == title)
                .Where(x => x.MediaType == type)
                .Where(x => x.Year == year)
                .Where(x => x.Page == page)
                .Where(x => DateTime.Now <= x.CreationDate.AddDays(1))
                .FirstOrDefault();

            if (query != null)
            {
                if (query.MovieList != null)
                {
                    serviceLogger.LogDebug("Znaleziono wpis w pamieci cache!");
                    var allMovies = movieCache.GetMovieShorts()
                        .Where(x => x.MovieListID == query.MovieList.ID).ToList();
                    return new MovieList(allMovies);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                serviceLogger.LogDebug("Nie znaleziono informacji o filmie w pamieci cache, pobieram z API...");
                MovieList newList = await movieRepository.GetMovieListByTitle(title, type, year, page);
                MovieListCache newMoveListCache = new(newList, title, (int)page, type, year);
                await movieCache.AddMovieListToDatabaseAsync(newMoveListCache, cancellationToken);
                return newList;
            }
        }
    }
}
