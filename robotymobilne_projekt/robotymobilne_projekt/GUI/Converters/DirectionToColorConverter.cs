using System;
using System.Globalization;
using System.Windows.Data;

namespace robotymobilne_projekt.GUI.Converters
{
    public class DirectionToColorConverter : IValueConverter
    {
        private const string forwardColor = "#CC119EDA";
        private const string backwardColor = "#CCDA1111";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is double)
            {
                double speed = (double)value;
                if(speed < 0)
                {
                    return backwardColor;
                }
            }
            return forwardColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
