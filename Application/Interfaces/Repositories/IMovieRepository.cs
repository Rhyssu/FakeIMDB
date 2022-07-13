using Domain.Entities;
using Domain.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IMovieRepository
    {
        Task<MovieInfo> GetMovieByTitle(string title,
                                            TypeOptions? type = null,
                                            int? year = null,
                                            PlotOptions plot = PlotOptions.Short);

        Task<MovieInfo> GetMovieByID(string id,
                                         TypeOptions? type = null,
                                         int? year = null,
                                         PlotOptions plot = PlotOptions.Short);

        Task<MovieList> GetMovieListByTitle(string title,
                                                  TypeOptions? type = null,
                                                  int? year = null); 
    }
}
