using Domain.Commons.Enums;
using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface IMovieService
    {
        Task<MovieInfo> GetMovieByID(string id, TypeOptions? type = null, int? year = null, PlotOptions plot = PlotOptions.Short);
        Task<MovieInfo> GetMovieByTitle(string title, TypeOptions? type = null, int? year = null, PlotOptions plot = PlotOptions.Short);
        Task<MovieList> GetMovieListByTitle(string title, TypeOptions? type = null, int? year = null);
    }
}
