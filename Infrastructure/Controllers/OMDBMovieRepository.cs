using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Core.Entities;
using Domain.Commons.Enums;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Controllers
{
    internal class OMDBMovieRepository : IMovieRepository
    {
        private readonly string APIKey;
        private readonly HttpClient client;
        private const string BaseUriAddress = "http://www.omdbapi.com";
        private readonly ILogger<OMDBMovieRepository> logger;

        public OMDBMovieRepository(HttpClient client, string APIKey, ILogger<OMDBMovieRepository> logger)
        {
            this.logger = logger;
            this.client = client;
            this.APIKey = APIKey;
            client.BaseAddress = new Uri(BaseUriAddress);
        }

        public async Task<MovieFullInfo> GetMovieByID(string id, TypeOptions? type = null, int? year = null, PlotOptions plot = PlotOptions.Short)
        {
            string query = ConstructGetByIDQuery(id, type, year, plot);
            string response = APIResponseBody(query).Result;
            try
            {
                MovieFullInfo movie = JsonSerializer.Deserialize<MovieFullInfo>(response);
                if (movie != null)
                {
                    return movie;
                }
            }
            catch (JsonException ex)
            {
                logger.LogError(ex, "Could not deserialize full movie info to class object");
            }

            throw new MovieNotFoundException();
        }

        public async Task<MovieFullInfo> GetMovieByTitle(string title, TypeOptions? type = null, int? year = null, PlotOptions plot = PlotOptions.Short)
        {
            string query = ConstructGetByTitleQuery(title, type, year, plot);
            string response = APIResponseBody(query).Result;
            try
            {
                HttpResponseMessage response = await client.GetAsync(addedQuery);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                if (responseBody.Contains("\"Response\": \"False\""))
                {
                    throw new MovieNotFoundException();
                }
                
                MovieFullInfo movie = JsonSerializer.Deserialize<MovieFullInfo>(responseBody);
                
                if (movie != null)
                {
                    return movie;
                }
            }
            catch (HttpRequestException ex)
            {
                logger.LogError(ex, "Could not retrieve full movie info.");
                throw new CouldNotRetrieveMoviesDataException();
            }
            catch (JsonException ex)
            {
                logger.LogError(ex, "Could not deserialize full movie info to class object.");
                throw new CouldNotRetrieveMoviesDataException();
            }

            return null;
        }

        public async Task<MovieSearchList> GetMovieListByTitle(string title, TypeOptions? type = null, int? year = null, int? page = null)
        {
            var movieParameters = new List<string>() { $"t={title}" };
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

            string addedQuery = string.Join("&", movieParameters);

            try
            {
                HttpResponseMessage response = await client.GetAsync(addedQuery);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                if (responseBody.Contains("\"Response\": \"False\""))
                {
                    throw new MovieNotFoundException();
                }

                MovieSearchList movieSearchList = JsonSerializer.Deserialize<MovieSearchList>(responseBody);

                if (movieSearchList != null)
                {
                    return movieSearchList;
                }
            }
            catch(HttpRequestException ex)
            {
                logger.LogError(ex, "Could not retrieve movie search list.");
                throw new CouldNotRetrieveMoviesDataException();
            }
            catch (JsonException ex)
            {
                logger.LogError(ex, "Could not deserialize movie search list to class object");
                throw new CouldNotRetrieveMoviesDataException();
            }

            return null;
        }

        private static string ConstructGetByTitleQuery(string title, TypeOptions? type = null, int? year = null, PlotOptions plot = PlotOptions.Short)
        {
            var movieParameters = new List<string>() { $"t={title}" };
            
            if (type.HasValue)
            {
                movieParameters.Add($"type={type}");
            }
            if (year.HasValue)
            {
                movieParameters.Add($"y={year}");
            }

            movieParameters.Add($"plot={plot}");
            return string.Join("&", movieParameters);
        }

        private static string ConstructGetByIDQuery(string id, TypeOptions? type = null, int? year = null, PlotOptions plot = PlotOptions.Short)
        {
            var movieParameters = new List<string>() { $"i={id}" };

            if (type.HasValue)
            {
                movieParameters.Add($"type={type}");
            }
            if (year.HasValue)
            {
                movieParameters.Add($"y={year}");
            }

            movieParameters.Add($"plot={plot}");
            return string.Join("&", movieParameters);
        }

        private static string ConstructMovieListQuery(string title, TypeOptions? type = null, int? year = null, int? page = null)
        {
            var movieParameters = new List<string> { $"s={title}" };
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

            return string.Join("&", movieParameters);
        }

        private async Task<string> APIResponseBody(string query)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(query);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

            } 
            catch (HttpRequestException ex)
            {
                logger.LogError(ex, "Could not retrieve movie search list.");
                throw new CouldNotRetrieveMoviesDataException();
            }
        }
    }
}
