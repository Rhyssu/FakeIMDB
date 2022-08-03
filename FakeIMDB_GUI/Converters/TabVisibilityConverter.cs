using FakeIMDB_GUI.Enums;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FakeIMDB_GUI.Converters
{
    public class TabVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var actualState = (SearchTypes)value;
            var targetState = (SearchTypes)parameter;

            if (actualState == targetState)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
