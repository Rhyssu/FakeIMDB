using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MovieInfo
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Awards { get; set; }
        public string Poster { get; set; }
        public Rating[] Ratings { get; set; }
        public string Metascore { get; set; }
        public string imdbRating { get; set; }
        public string imdbVotes { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string DVD { get; set; }
        public string BoxOffice { get; set; }
        public string Production { get; set; }
        public string Website { get; set; }
        public string Response { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Title        : {Title}");
            stringBuilder.AppendLine($"Year         : {Year}");
            stringBuilder.AppendLine($"Rated        : {Rated}");
            stringBuilder.AppendLine($"Released     : {Released}");
            stringBuilder.AppendLine($"Runtime      : {Runtime}");
            stringBuilder.AppendLine($"Genre        : {Genre}");
            stringBuilder.AppendLine($"Director     : {Director}");
            stringBuilder.AppendLine($"Writer       : {Writer}");
            stringBuilder.AppendLine($"Actors       : {Actors}");
            stringBuilder.AppendLine($"Plot         : {Plot}");
            stringBuilder.AppendLine($"Language     : {Language}");
            stringBuilder.AppendLine($"Country      : {Country}");
            stringBuilder.AppendLine($"Awards       : {Awards}");
            stringBuilder.AppendLine($"Poster       : {Poster}");
            stringBuilder.AppendLine($"Metascore    : {Metascore}");
            stringBuilder.AppendLine($"imdbRating   : {imdbRating}");
            stringBuilder.AppendLine($"imdbVotes    : {imdbVotes}");
            stringBuilder.AppendLine($"imdbID       : {imdbID}");
            stringBuilder.AppendLine($"Type         : {Type}");
            stringBuilder.AppendLine($"DVD          : {DVD}");
            stringBuilder.AppendLine($"BoxOffice    : {BoxOffice}");
            stringBuilder.AppendLine($"Production   : {Production}");
            stringBuilder.AppendLine($"Website      : {Website}");
            stringBuilder.AppendLine($"Response     : {Response}");
            return stringBuilder.ToString();
        }
    }

    public class Rating
    {
        public string Source { get; set; }
        public string Value { get; set; }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Source       : {Source}");
            stringBuilder.AppendLine($"value        : {Value}");
            return stringBuilder.ToString();
        }
    }
}
