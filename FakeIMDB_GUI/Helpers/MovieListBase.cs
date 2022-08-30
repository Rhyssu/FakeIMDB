using Domain.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;

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
        public int CurrentPage
        {
            get => GetBackingValue<int>();
            set => SetBackingValue(value);
        }
    }
}