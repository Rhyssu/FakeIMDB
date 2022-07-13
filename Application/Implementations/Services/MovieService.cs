using Application.Interfaces.Services;
using Domain.Commons.Enums;
using Domain.Entities;

namespace Application.Implementations.Services
{
    public class MovieService : IMovieService
    {
        public Task<MovieInfo> GetMovieByID(string id, TypeOptions? type = null, int? year = null, PlotOptions plot = PlotOptions.Short)
        {
            throw new NotImplementedException();
        }

        public Task<MovieInfo> GetMovieByTitle(string title, TypeOptions? type = null, int? year = null, PlotOptions plot = PlotOptions.Short)
        {
            throw new NotImplementedException();
        }
    }
}
