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
            => context.MovieInfo.Include(x => x.MovieInfo);

        public IQueryable<MovieListCache> GetAllMoviesLists()
            => context.MovieList.Include(x => x.MovieList);

        public async Task AddMovieToDatabaseAsync(MovieInfo movie)
        {
            context.Add(movie);
            await context.SaveChangesAsync();
        }
        public async Task AddMovieListToDatabaseAsync(MovieList movieList)
        {
            context.Add(movieList);
            await context.SaveChangesAsync();
        }
    }
}
