using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contexts
{
    public class MovieCacheContext : DbContext
    {
        public DbSet<MovieInfoCache> MovieInfo { get; set; }
        public DbSet<MovieListCache> MovieList { get; set; }

        public MovieCacheContext() => this.Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("MoviesDatabase");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MovieListCache>()
                .HasKey(x => x.ID);

            modelBuilder.Entity<MovieInfoCache>()
                .HasKey(x => x.ID);
        }
    }
}
