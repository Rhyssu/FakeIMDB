using System.Text.Json;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Commons.Enums;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;
using Infrastructure.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Infrastructure.Common;

namespace Infrastructure.Repositories
{
    public class OMDBMovieRepository : IMovieRepository
    {
        private readonly APISettings apiSettings;
        private readonly HttpClient client;
        private readonly ILogger<OMDBMovieRepository> logger;
        private const string BaseUriAddress = "http://www.omdbapi.com";

        public OMDBMovieRepository(IHttpClientFactory clientFactory, IOptions<APISettings> options, ILogger<OMDBMovieRepository> logger)
        {
            this.client = clientFactory.CreateClient();
            this.apiSettings = options.Value;
            this.logger = logger;
            client.BaseAddress = new Uri(BaseUriAddress);
        }

        public async Task<MovieInfo> GetMovieByID(
            string id,
            TypeOptions? type = null,
            int? year = null,
            PlotOptions plot = PlotOptions.Short,
            CancellationToken cancellationToken = default)
        {
            string query = ConstructGetByIDQuery(id, type, year, plot);
            string response = await APIResponseBody(query, cancellationToken);
            TryToDeserialize<MovieInfo>(response, out var newMovie);
            return newMovie;
        }

        public async Task<MovieInfo> GetMovieByTitle(
            string title,
            TypeOptions? type = null,
            int? year = null,
            PlotOptions plot = PlotOptions.Short,
            CancellationToken cancellationToken = default)
        {
            string query = ConstructGetByTitleQuery(title, type, year, plot);
            string response = await APIResponseBody(query, cancellationToken);
            TryToDeserialize<MovieInfo>(response, out var newMovie);
            return newMovie;
        }

        public async Task<MovieList> GetMovieListByTitle(
            string title,
            TypeOptions? type = null,
            int? year = null,
            int? page = null,
            CancellationToken cancellationToken = default)
        {
            string query = ConstructMovieListQuery(title, type, year, page);
            string response = await APIResponseBody(query, cancellationToken);
            TryToDeserialize<MovieList>(response, out var newList);
            return newList;
        }

        private string ConstructGetByTitleQuery(
            string title,
            TypeOptions? type = null,
            int? year = null,
            PlotOptions plot = PlotOptions.Short)
        {
            var movieParameters = new List<string>() { $"/?t={title}" };
            
            if (type.HasValue)
            {
                movieParameters.Add($"type={type}");
            }
            if (year.HasValue)
            {
                movieParameters.Add($"y={year}");
            }

            movieParameters.Add($"plot={plot}");
            movieParameters.Add($"apikey={apiSettings.APIKey}");
            return string.Join("&", movieParameters);
        }

        private string ConstructGetByIDQuery(
            string id,
            TypeOptions? type = null,
            int? year = null,
            PlotOptions plot = PlotOptions.Short)
        {
            var movieParameters = new List<string>() { $"/?i={id}" };

            if (type.HasValue)
            {
                movieParameters.Add($"type={type}");
            }
            if (year.HasValue)
            {
                movieParameters.Add($"y={year}");
            }

            movieParameters.Add($"plot={plot}");
            movieParameters.Add($"apikey={apiSettings.APIKey}");
            return string.Join("&", movieParameters);
        }

        private string ConstructMovieListQuery(
            string title,
            TypeOptions? type = null,
            int? year = null,
            int? page = null)
        {
            var movieParameters = new List<string> { $"/?s={title}" };
            if (type.HasValue)
            {
                movieParameters.Add($"type={type}");
            }
            if (year.HasValue)
            {
                movieParameters.Add($"y={year}");
            }
            if (page.HasValue)
            {
                movieParameters.Add($"page={page}");
            }

            movieParameters.Add($"apikey={apiSettings.APIKey}");
            return string.Join("&", movieParameters);
        }

        private async Task<string> APIResponseBody(string query, CancellationToken cancellationToken = default)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(query, cancellationToken);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
                return responseBody;

            } 
            catch (HttpRequestException ex)
            {
                logger.LogError(ex, "Could not retrieve movie search list.");
                throw new CouldNotRetrieveMoviesDataException();
            }
        }
        private bool TryToDeserialize<T>(string json, out T movie) where T : class
        {
            try
            {
                ErrorResponse errorResponse = JsonSerializer.Deserialize<ErrorResponse>(json);
                var isError = string.IsNullOrEmpty(errorResponse.Error);
                if (isError)
                {
                    movie = JsonSerializer.Deserialize<T>(json);
                    return true;
                }
                else
                {
                    logger.LogDebug("Could not find a movie {@APIError}", errorResponse.Error);
                }
            }
            catch (JsonException ex)
            {
                logger.LogError(ex, "API response error.");
            }
            movie = null;
            return false;
        }
    }
}
