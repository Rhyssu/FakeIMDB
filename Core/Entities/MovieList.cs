using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MovieList
    {
        public List<MovieShortInfo> Search { get; set; }
        public string totalResults { get; set; }
        public string Response { get; set; }
        public Guid ID { get; } = Guid.NewGuid();

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (Search != null)
            {
                foreach (MovieShortInfo shortInfo in Search)
                {
                    stringBuilder.Append(shortInfo.ToString());
                }
            }
            stringBuilder.Append($"\ntotalResults: {totalResults}, \nResponse: {Response}");
            return stringBuilder.ToString();
        }
    }
    public class MovieShortInfo
    {
        public string Title { get; set; }
        public string Year { get; set; } 
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Title        : {Title}");
            stringBuilder.AppendLine($"Year         : {Year}");
            stringBuilder.AppendLine($"imdbID       : {imdbID}");
            stringBuilder.AppendLine($"Type         : {Type}");
            stringBuilder.AppendLine($"Poster       : {Poster}");
            return stringBuilder.ToString();
        }
    }
}
