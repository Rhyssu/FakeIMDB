using Application.Interfaces.Services;
using Domain.Commons.Enums;
using Domain.Entities;
using FakeIMDB_GUI.Commands;
using FakeIMDB_GUI.Enums;
using FakeIMDB_GUI.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace FakeIMDB_GUI.ViewModels
{
    public static class Extensions
    {
        public static T Next<T>(this T src) where T : Enum
        {
            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) + 1;
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }
    }

    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged, INotifyDataErrorInfo
    {
        #region Variable Declaration

        private readonly IMovieService movieService;

        public SearchTypes SearchState
        {
            get => GetBackingValue<SearchTypes>();
            set
            {
                SetBackingValue(value);
                RaisePropertyChanged(nameof(SearchOptionLabel));
            }
        }

        public TypeOptions MediaType
        {
            get => GetBackingValue<TypeOptions>();
            set => SetBackingValue(value);
        }

        public string ResultTextBlock
        {
            get => GetBackingValue<string>();
            set => SetBackingValue(value);
        }

        public MovieInfo MovieInfo
        {
            get => GetBackingValue<MovieInfo>();
            set => SetBackingValue(value);
        }

        public MovieList MovieList
        {
            get => GetBackingValue<MovieList>();
            set => SetBackingValue(value);
        }

        public string Year
        {
            get => GetBackingValue<string>();
            set => SetBackingValue(value, YearValidator);
        }

        public MovieListBase MovieListBase
        {
            get => GetBackingValue<MovieListBase>();
            set => SetBackingValue(value);
        }

        public string SearchOptionLabel =>
            SearchState switch
            {
                SearchTypes.ByID => "ByID: ",
                SearchTypes.ByTitle => "ByTitle: ",
                SearchTypes.BySearch => "BySearch: ",
                _ => "",
            };

        public string Title
        {
            get => GetBackingValue<string>();
            set => SetBackingValue(value, TitleValidator);
        }

        public TypeOptions[] AllMediaTypes { get; } = Enum.GetValues<TypeOptions>();

        public MyCommand BySearch { get; set; }
        public MyCommand ByTitle { get; set; }
        public MyCommand ByID { get; set; }
        public MyCommand Search { get; set; }
        public MyCommand Reset { get; set; }
        public MyCommand PreviousPage { get; set; }
        public MyCommand NextPage { get; set; }
        public MyCommand SearchSelectedListBoxItem { get; set; }
        public MyCommand NextTab { get; set; }

        #endregion Variable Declaration

        public MainWindowViewModel(IMovieService movieService)
        {
            this.movieService = movieService;
            Title = null;
            Year = null;

            InitializeCommands();
        }

        #region Commands

        private void InitializeCommands()
        {
            ByIDCommand();
            ByTitleCommand();
            BySearchCommand();
            SearchCommand();
            ResetCommand();
            NextPageCommand();
            PreviousPageCommand();
            SearchSelectedListBoxItemCommand();
            NextTabCommand();
        }

        private void ByIDCommand()
        {
            ByID = new MyCommand(
                (_) => SearchState = SearchTypes.ByID,
                (_) => SearchState != SearchTypes.ByID);
        }

        private void ByTitleCommand()
        {
            ByTitle = new MyCommand(
                (_) => SearchState = SearchTypes.ByTitle,
                (_) => SearchState != SearchTypes.ByTitle);
        }

        private void BySearchCommand()
        {
            BySearch = new MyCommand(
                (_) => SearchState = SearchTypes.BySearch,
                (_) => SearchState != SearchTypes.BySearch);
        }

        private void SearchCommand()
        {
            Search = new MyCommand(
                async (_) =>
                {
                    MovieListBase = new MovieListBase();
                    MovieListBase.CurrentPage = 1;

                    if (SearchState.ToString() == "ByID")
                    {
                        MovieInfo = await movieService.GetMovieByID(Title);
                    }
                    else if (SearchState.ToString() == "ByTitle")
                    {
                        MovieInfo = await movieService.GetMovieByTitle(Title, GetValidType(), GetValidYear());
                    } 
                    else if (SearchState.ToString() == "BySearch")
                    {
                        MovieList = await movieService.GetMovieListByTitle(Title, GetValidType(), GetValidYear(), 1);
                        if (MovieList != null)
                        {
                            MovieListBase.CurrentPage = 1;
                            MovieListBase.ShortMovieInfo = new ObservableCollection<MovieShortInfo>(MovieList.Search);
                        }
                    }
                });
        }

        private void NextPageCommand()
        {
            NextPage = new MyCommand(
                async (_) =>
                {
                    var tempCurrentPage = MovieListBase.CurrentPage + 1;
                    MovieList = await movieService.GetMovieListByTitle(Title, GetValidType(), GetValidYear(), tempCurrentPage);
                    if (MovieList != null)
                    {
                        MovieListBase.CurrentPage = tempCurrentPage;
                        MovieListBase.ShortMovieInfo = new ObservableCollection<MovieShortInfo>(MovieList.Search);
                    }
                });
        }

        private void PreviousPageCommand()
        {
            PreviousPage = new MyCommand(
                async (_) =>
                {
                    if (MovieListBase.CurrentPage > 1)
                    {
                        MovieList = await movieService.GetMovieListByTitle(Title, GetValidType(), GetValidYear(), --MovieListBase.CurrentPage);
                        if (MovieList != null)
                        {
                            MovieListBase.ShortMovieInfo = new ObservableCollection<MovieShortInfo>(MovieList.Search);
                        }
                    }
                });
        }

        private void SearchSelectedListBoxItemCommand()
        {
            SearchSelectedListBoxItem = new MyCommand(
                async (_) =>
                {
                    SearchState = SearchTypes.ByID;
                    MovieInfo = await movieService.GetMovieByID(MovieListBase.SelectedMovieInfo.imdbID);
                });
        }

        private void NextTabCommand()
        {
            NextTab = new MyCommand(
                (_) =>
                {
                    SearchState = SearchState.Next();
                });
        }

        private void ResetCommand()
        {
            Reset = new MyCommand(
                (_) =>
                {
                    Title = null;
                    Year = null;
                    MediaType = TypeOptions.All;
                }
                );
        }

        private TypeOptions? GetValidType()
        {
            if (MediaType != TypeOptions.All)
            {
                return MediaType;
            }
            return null;
        }

        private int? GetValidYear()
        {
            if (int.TryParse(Year, out int YearInt))
            {
                return YearInt;
            }
            return null;
        }

        #endregion Commands

        #region Validation

        public static IEnumerable<string> TitleValidator<TProperty>(TProperty value)
        {
            if (string.IsNullOrWhiteSpace(value as string))
                yield return "Title can't be empty";
        }

        public static IEnumerable<string> YearValidator<TProperty>(TProperty value)
        {
            var valueStr = value as string;

            if (!string.IsNullOrEmpty(valueStr))
            {
                if (!Regex.IsMatch(valueStr, "^[0-9]{4}$"))
                {
                    yield return "Must be 4digits string";
                }
            }
        }

        #endregion Validation
    }
}
