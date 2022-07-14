using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Commons.Enums;

namespace Domain.Entities
{
    public record MovieInfoCache
    {
        public MovieInfoCache(MovieInfo movie, string query, TypeOptions? mediaType = null, int? year = null, PlotOptions plotOptions = PlotOptions.Short)
        {
            this.Year = year;
            this.MovieInfo = movie;
            this.MediaType = mediaType;
            this.Query = query;
            this.PlotOption = plotOptions;
        }
        public MovieInfoCache()
        {

        }
        public MovieInfo MovieInfo { get; init; }
        public string Query { get; set; }
        public TypeOptions? MediaType { get; init; }
        public int? Year { get; init; }
        public Guid ID { get; } = Guid.NewGuid();
        public PlotOptions PlotOption { get; init; }
        public DateTime CreationDate { get; } = DateTime.UtcNow;
    }
}
