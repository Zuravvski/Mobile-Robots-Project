using System;
using System.Globalization;
using System.Windows.Data;

namespace robotymobilne_projekt.GUI.Converters
{
    public class ReverseConnectionStatus : IValueConverter
    {
        ReverseLogicConverter reverseConverter;
        RobotStatusToBoolConverter statusConverter;

        public ReverseConnectionStatus()
        {
            reverseConverter = new ReverseLogicConverter();
            statusConverter = new RobotStatusToBoolConverter();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool status = (bool)statusConverter.Convert(value, targetType, parameter, culture);
            return reverseConverter.Convert(status, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
