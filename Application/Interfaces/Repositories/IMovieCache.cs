using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Commons.Enums;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IMovieCache
    {
        IQueryable<MovieInfoCache> GetAllMovies();
        IQueryable<MovieListCache> GetAllMoviesLists();
        IQueryable<MovieShortInfo> GetMovieShorts();
        Task AddMovieToDatabaseAsync (MovieInfoCache movie, CancellationToken cancellationToken = default);
        Task AddMovieListToDatabaseAsync (MovieListCache movieList, CancellationToken cancellationToken = default);
    }
}
