using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class MovieList
    {
        public MovieList(List<MovieShortInfo> movieShortInfos)
        {
            Search = movieShortInfos;
        }
        public MovieList()
        {

        }
        public List<MovieShortInfo> Search { get; set; }
        public string totalResults { get; set; }
        public string Response { get; set; }
        public Guid ID { get; } = Guid.NewGuid();
    }
    public class MovieShortInfo
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
        public Guid MovieListID { get; set; }
    }
}
