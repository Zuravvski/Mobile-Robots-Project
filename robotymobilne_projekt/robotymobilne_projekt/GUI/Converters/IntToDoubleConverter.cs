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
                result = System.Convert.ToDouble(value);
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result = 0;
            if (value is double)
            {
                result = System.Convert.ToInt32(value);
            }
            return result;
        }
    }
}
