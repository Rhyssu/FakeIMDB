using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace FakeIMDB_GUI.Converters
{
    public class StringToSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string valueStr)
            {
                // If there is no poster available from the API return default image 
                if (valueStr == "N/A")
                {
                    return new ImageSourceConverter().ConvertFromString("https://i.imgur.com/H9e1jV7.png");
                }
                else
                {
                    return new ImageSourceConverter().ConvertFromString(valueStr);
                }
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
