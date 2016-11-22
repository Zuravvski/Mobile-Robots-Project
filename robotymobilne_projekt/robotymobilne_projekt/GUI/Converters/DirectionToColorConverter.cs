using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace robotymobilne_projekt.GUI.Converters
{
    public class DirectionToColorConverter : IValueConverter
    {
        private const string forwardColorHex = "#CC119EDA";
        private const string backwardColorHex = "#CCDA1111";
        private readonly SolidColorBrush forwardColor;
        private readonly SolidColorBrush backwardColor;

        public DirectionToColorConverter()
        {
            BrushConverter converter = new BrushConverter();
            forwardColor = (SolidColorBrush)converter.ConvertFrom(forwardColorHex);
            backwardColor = (SolidColorBrush)converter.ConvertFrom(backwardColorHex);
        }

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
