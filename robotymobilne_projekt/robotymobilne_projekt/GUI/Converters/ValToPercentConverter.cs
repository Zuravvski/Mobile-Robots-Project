using System;
using System.Globalization;
using System.Windows.Data;
using robotymobilne_projekt.Manual;

namespace robotymobilne_projekt.GUI.Converters
{
    public class ValToPercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is int)
            {
                double toConvert = (int)value;
                return AbstractController.mapValues(toConvert, 4300, 5000, 0, 100);
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                double toConvert = (double)value;
                return AbstractController.mapValues(toConvert, 0, 100, 4300, 5000);
            }
            return 0;
        }
    }
}
