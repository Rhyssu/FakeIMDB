using Domain.Commons.Enums;

namespace Domain.Entities
{
    public record MovieListCache
    {
        public MovieList MovieList { get; init; }
        public string Title { get; init; } 
        public DateTime CreationDate { get; init; }
        public TypeOptions MediaType { get; init; }
        public int Year { get; init; }
        public Guid ID { get; } = Guid.NewGuid();
    }
}
