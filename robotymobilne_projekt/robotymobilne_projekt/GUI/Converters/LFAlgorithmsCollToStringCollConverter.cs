using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using robotymobilne_projekt.Automatic.LineFollower;

namespace robotymobilne_projekt.GUI.Converters
{
    public class LFAlgorithmsCollToStringCollConverter : IValueConverter
    {
        private const string PID = "PID";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = new List<string>() { PID };
            if (!(value is IEnumerable<LineFollowerAlgorithm.Type>)) return result;

            var algorithms = (List<LineFollowerAlgorithm.Type>)value;
            foreach (var algorithm in algorithms)
            {
                switch (algorithm)
                {
                    default:
                    case LineFollowerAlgorithm.Type.PID:
                        result.Add(PID);
                        break;
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
