using Domain.Entities;
using FakeIMDB_GUI.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeIMDB_GUI.Helpers
{
    public class MovieListBase : ViewModelBase, INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public ObservableCollection<MovieShortInfo> ShortMovieInfo
        {
            get => GetBackingValue<ObservableCollection<MovieShortInfo>>();
            set => SetBackingValue(value);
        }
        public MovieShortInfo SelectedMovieInfo
        {
            get => GetBackingValue<MovieShortInfo>();
            set => SetBackingValue(value);
        }
        public string PosterSource
        {
            get => GetBackingValue<string>();
            set => SetBackingValue(value);
        }
        public int CurrentPage
        {
            get => GetBackingValue<int>();
            set => SetBackingValue(value);
        }
    }
}
