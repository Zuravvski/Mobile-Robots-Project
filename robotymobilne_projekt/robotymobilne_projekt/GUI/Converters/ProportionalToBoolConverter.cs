using System;
using System.Globalization;
using System.Windows.Data;
using robotymobilne_projekt.Automatic.LineFollower;

namespace robotymobilne_projekt.GUI.Converters
{
    public class ProportionalToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (LineFollowerAlgorithm.Type) value;
            return value is LineFollowerAlgorithm.Type && (type == LineFollowerAlgorithm.Type.P
                || type == LineFollowerAlgorithm.Type.PID);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
