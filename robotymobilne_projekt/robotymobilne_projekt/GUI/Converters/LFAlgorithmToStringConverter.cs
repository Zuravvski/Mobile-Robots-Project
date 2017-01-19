using System;
using System.Globalization;
using System.Windows.Data;
using robotymobilne_projekt.Automatic.LineFollower;

namespace robotymobilne_projekt.GUI.Converters
{
    public class LFAlgorithmToStringConverter : IValueConverter
    {
        private const string Custom = "PID";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = Custom;
            if(value is LineFollowerAlgorithm.Type)
            {
                var algorithm = (LineFollowerAlgorithm.Type) value;
                switch (algorithm)
                {
                    case LineFollowerAlgorithm.Type.PID:
                        result = Custom;
                        break;
                }
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = LineFollowerAlgorithm.Type.PID;
            if (value is string)
            {
                var algorithm = (string) value;
                switch (algorithm)
                {
                    case Custom:
                        result = LineFollowerAlgorithm.Type.PID;
                        break;
                }
            }
            return result;
        }
    }
}
