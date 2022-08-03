using FakeIMDB_GUI.Commands;
using FakeIMDB_GUI.Enums;
using FakeIMDB_GUI.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Reflection;

namespace FakeIMDB_GUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public SearchTypes SearchState
        {
            get => GetBackingValue<SearchTypes>();
            set
            {
                SetBackingValue(value);
                RaisePropertyChanged(nameof(SearchOptionLabel));
            }
        }

        public MediaTypes MediaType
        {
            get => GetBackingValue<MediaTypes>();
            set => SetBackingValue(value);
        }

        public string ResultTextBlock
        {
            get => GetBackingValue<string>();
            set => SetBackingValue(value);
        }

        public ObservableCollection<SolidColorBrush> SolidColorBrushesList { get; } = new();

        public MediaTypes[] AllMediaTypes { get; } = Enum.GetValues<MediaTypes>();

        public string Year
        {
            get => GetBackingValue<string>();
            set => SetBackingValue(value, YearValidator);
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

        public MyCommand BySearchCommand { get; init; }
        public MyCommand ByTitleCommand { get; init; }
        public MyCommand ByIDCommand { get; init; }
        public MyCommand SearchCommand { get; init; }
        public MyCommand ResetCommand { get; init; }

        public MainWindowViewModel()
        {
            ByIDCommand = new MyCommand(
                (_) => SearchState = SearchTypes.ByID,
                (_) => SearchState != SearchTypes.ByID);
            ByTitleCommand = new MyCommand(
                (_) => SearchState = SearchTypes.ByTitle,
                (_) => SearchState != SearchTypes.ByTitle);
            BySearchCommand = new MyCommand(
                (_) => SearchState = SearchTypes.BySearch,
                (_) => SearchState != SearchTypes.BySearch);
            ResetCommand = new MyCommand(
                (_) =>
                {
                    Title = null;
                    Year  = null;
                    MediaType = MediaTypes.All;
                }
                );
             Year  = null;
             Title = null;

            PropertyInfo[] colorBrushProperties = typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static);
            foreach(PropertyInfo colorProperty in colorBrushProperties)
            {
                SolidColorBrushesList.Add(new SolidColorBrush((Color) colorProperty.GetValue(this, null)));
            }
        }

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
    }
}
