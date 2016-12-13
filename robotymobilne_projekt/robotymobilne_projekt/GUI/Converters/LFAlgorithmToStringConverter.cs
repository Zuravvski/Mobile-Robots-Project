using System;
using System.Globalization;
using System.Windows.Data;
using robotymobilne_projekt.Automatic.LineFollower;

namespace robotymobilne_projekt.GUI.Converters
{
    public class LFAlgorithmToStringConverter : IValueConverter
    {
        private const string P = "P";
        private const string Custom = "Custom";
        private const string PID = "PID";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = P;
            if(value is LineFollowerAlgorithm.Type)
            {
                var algorithm = (LineFollowerAlgorithm.Type) value;
                switch (algorithm)
                {
                    case LineFollowerAlgorithm.Type.PID:
                        result = PID;
                        break;

                    case LineFollowerAlgorithm.Type.CUSTOM:
                        result = Custom;
                        break;
                }
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = LineFollowerAlgorithm.Type.P;
            if (value is string)
            {
                var algorithm = (string) value;
                switch (algorithm)
                {
                    case PID:
                        {
                            result = LineFollowerAlgorithm.Type.PID;
                            break;
                        }
                }
            }
            return result;
        }
    }
}
