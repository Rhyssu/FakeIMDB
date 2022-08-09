using Domain.Commons.Enums;

namespace Domain.Entities
{
    public record MovieListCache
    {
        public MovieListCache(MovieList movieList, string title, int page, TypeOptions? mediaType = null, int? year = null)
        {
            this.Year = year;
            this.QueryTitle = title;
            this.Page = page;
            this.MovieList = movieList;
            this.MediaType = mediaType;
            this.CreationDate = DateTime.UtcNow;
        }
        public MovieListCache()
        {

        }
        public int? Year { get; init; }
        public string QueryTitle { get; init; }
        public Guid ID { get; } = Guid.NewGuid();
        public MovieList MovieList { get; init; }
        public TypeOptions? MediaType { get; init; }
        public DateTime CreationDate { get; init; }
        public int? Page { get; init; }
    }
}
