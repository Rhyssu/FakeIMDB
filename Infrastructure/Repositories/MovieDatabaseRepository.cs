using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class MovieDatabaseRepository : IMovieCache
    {
        private readonly MovieCacheContext context;

        public MovieDatabaseRepository(MovieCacheContext context)
        {
            this.context = context;
        }

        public IQueryable<MovieInfoCache> GetAllMovies() 
            => context.MovieInfoCaches.Include(x => x.MovieInfo);

        public IQueryable<MovieListCache> GetAllMoviesLists()
            => context.MovieListCaches.Include(x => x.MovieList);
        public IQueryable<MovieShortInfo> GetMovieShorts() => context.MovieShortInfos;

        public async Task AddMovieToDatabaseAsync(MovieInfoCache movie, CancellationToken cancellationToken = default)
        {
            await context.AddAsync(movie, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddMovieListToDatabaseAsync(MovieListCache movieList, CancellationToken cancellationToken = default)
        {
            await context.AddAsync(movieList, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
