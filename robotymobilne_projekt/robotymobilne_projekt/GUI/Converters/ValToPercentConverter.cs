using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace robotymobilne_projekt.GUI.Converters
{
    public class ValToPercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is double)
            {
                double toConvert = (double)value;
                return mapValues(toConvert, 4300, 5000, 0, 100);
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                double toConvert = (double)value;
                return mapValues(toConvert, 0, 100, 4300, 5000);
            }
            return 0;
        }

        private int mapValues(double value, double fromSource, double toSource, double fromTarget, double toTarget)
        {
            return (int)((value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget);
        }
    }
}
