using Domain.Commons.Enums;
using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface IMovieService
    {
        Task<MovieInfo> GetMovieByID(string id,
            TypeOptions? type = null,
            int? year = null,
            PlotOptions plot = PlotOptions.Short,
            CancellationToken cancellationToken = default);
        Task<MovieInfo> GetMovieByTitle(string title,
            TypeOptions? type = null,
            int? year = null,
            PlotOptions plot = PlotOptions.Short,
            CancellationToken cancellationToken = default);
        Task<MovieList> GetMovieListByTitle(string title,
            TypeOptions? type = null,
            int? year = null,
            int? page = null, 
            CancellationToken cancellationToken = default);
    }
}
