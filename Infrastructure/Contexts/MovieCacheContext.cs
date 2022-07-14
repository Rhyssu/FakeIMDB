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
        public DbSet<MovieInfoCache> MovieInfoCaches { get; set; }
        public DbSet<MovieListCache> MovieListCaches { get; set; } 
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

            modelBuilder.Entity<Rating>()
                .HasKey(x => x.ID);

            modelBuilder.Entity<MovieInfo>()
                .HasKey(x => x.ID);

            modelBuilder.Entity<MovieInfoCache>()
                .HasKey(x => x.ID);

            modelBuilder.Entity<MovieShortInfo>()
                .HasKey(x => x.ID);

            modelBuilder.Entity<MovieList>()
                .HasKey(x => x.ID);

            modelBuilder.Entity<MovieListCache>()
                .HasKey(x => x.ID);
        }
    }
}
