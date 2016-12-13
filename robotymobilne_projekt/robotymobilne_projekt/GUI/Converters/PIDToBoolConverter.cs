using System;
using System.Globalization;
using System.Windows.Data;
using robotymobilne_projekt.Automatic.LineFollower;

namespace robotymobilne_projekt.GUI.Converters
{
    public class PIDToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is LineFollowerAlgorithm.Type && (LineFollowerAlgorithm.Type)value == LineFollowerAlgorithm.Type.PID;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
