using System;
using System.Globalization;
using System.Windows.Data;

namespace robotymobilne_projekt.GUI.Converters
{
    public class IntToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 0;
            if (value is int)
            {
                result = (int)value;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result = 0;
            if (value is double)
            {
                result = (int) value;
            }
            return result;
        }
    }
}
