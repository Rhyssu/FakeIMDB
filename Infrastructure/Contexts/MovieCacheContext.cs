using Domain.Entities;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.Contexts
{
    public class MovieCacheContext : DbContext
    {
        public DatabaseSettings config { get; set; }
        public DbSet<MovieInfoCache> MovieInfoCaches { get; set; }
        public DbSet<MovieListCache> MovieListCaches { get; set; }
        public MovieCacheContext(IOptions<DatabaseSettings> options)
        {
            config = options.Value;
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (config.UseInMemoryDatabase)
                    optionsBuilder.UseInMemoryDatabase("MoviesDatabase");
                else
                    optionsBuilder.UseSqlServer(config.ConnectionString);
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
