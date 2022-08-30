using Domain.Entities;
using FakeIMDB_GUI.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace FakeIMDB_GUI.CustomControls
{
    public class MovieListContentControl : ContentControl
    {       
        public ObservableCollection<MovieShortInfo> MovieList
        {
            get { return (ObservableCollection<MovieShortInfo>)GetValue(MovieListProperty); }
            set { SetValue(MovieListProperty, value); }
        }

        public MyCommand NextPageCommand
        {
            get { return (MyCommand)GetValue(NextPageProperty); }
            set { SetValue(NextPageProperty, value); }
        }

        public MyCommand SearchSelected
        {
            get { return (MyCommand)GetValue(SearchSelectedProperty); }
            set { SetValue(SearchSelectedProperty, value); }
        }

        public MyCommand PreviousPageCommand
        {
            get { return (MyCommand)GetValue(PreviousPageProperty); }
            set { SetValue(PreviousPageProperty, value); }
        }

        public MovieShortInfo SelectedMovieInfo
        {
            get { return (MovieShortInfo)GetValue(SelectedMovieInfoProperty); }
            set { SetValue(SelectedMovieInfoProperty, value); }
        }

        public int CurrentPage
        {
            get { return (int)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        public static readonly DependencyProperty MovieListProperty =
            DependencyProperty.Register(
                nameof(MovieList), 
                typeof(ObservableCollection<MovieShortInfo>), 
                typeof(MovieListContentControl), 
                new PropertyMetadata(default));

        public static readonly DependencyProperty SelectedMovieInfoProperty =
            DependencyProperty.Register(
                nameof(SelectedMovieInfo),
                typeof(MovieShortInfo),
                typeof(MovieListContentControl),
                new PropertyMetadata(default));

        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(
                nameof(CurrentPage),
                typeof(int),
                typeof(MovieListContentControl),
                new PropertyMetadata(default));

        public static readonly DependencyProperty NextPageProperty =
            DependencyProperty.Register(
                nameof(NextPageCommand),
                typeof(MyCommand),
                typeof(MovieListContentControl),
                new PropertyMetadata(default));

        public static readonly DependencyProperty SearchSelectedProperty =
            DependencyProperty.Register(
                nameof(SearchSelected),
                typeof(MyCommand),
                typeof(MovieListContentControl),
                new PropertyMetadata(default));

        public static readonly DependencyProperty PreviousPageProperty =
            DependencyProperty.Register(
                nameof(PreviousPageCommand),
                typeof(MyCommand),
                typeof(MovieListContentControl),
                new PropertyMetadata(default));
    }
}
