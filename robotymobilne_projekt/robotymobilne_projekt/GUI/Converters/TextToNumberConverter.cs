using System;
using System.Globalization;
using System.Windows.Data;

namespace robotymobilne_projekt.GUI.Converters
{
    public class TextToNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result;
            const int defaultValue = 3;
            bool isValid = int.TryParse(value.ToString(), out result);
            return isValid ? result : defaultValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
