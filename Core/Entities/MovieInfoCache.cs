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
        public MovieInfo MovieInfo { get; init; }
        public DateTime CreationDate { get; init; }
        public TypeOptions MediaType { get; init; }
        public int Year { get; init; }
        public PlotOptions PlotOption { get; init; }
        public Guid ID { get; } = Guid.NewGuid();
    }
}
