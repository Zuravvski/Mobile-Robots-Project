using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace robotymobilne_projekt.GUI.Converters
{
    public class BatteryStatusToColorConverter : IValueConverter
    {
        private const string greenColorHex = "#FF008000";
        private const string redColorHex = "#FFFF0000";
        private readonly SolidColorBrush goodLevel;
        private readonly SolidColorBrush lowLevel;

        public BatteryStatusToColorConverter()
        {
            var converter = new BrushConverter();
            goodLevel = (SolidColorBrush)converter.ConvertFrom(greenColorHex);
            lowLevel = (SolidColorBrush)converter.ConvertFrom(redColorHex);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is int)
            {
                double toConvert = (int)value;
                int result = mapValues(toConvert, 4300, 5000, 0, 100);
                if(result > 20)
                {
                    return goodLevel;
                }
            }
            return lowLevel;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        private int mapValues(double value, double fromSource, double toSource, double fromTarget, double toTarget)
        {
            return (int)((value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget);
        }
    }
}
