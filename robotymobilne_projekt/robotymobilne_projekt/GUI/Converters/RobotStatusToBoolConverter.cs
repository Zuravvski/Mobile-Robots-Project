using System;
using System.Globalization;
using System.Windows.Data;
using robotymobilne_projekt.Devices;

namespace robotymobilne_projekt.GUI.Converters
{
    public class RobotStatusToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is RobotModel.StatusE)
            {
                var status = (RobotModel.StatusE)value;
                if(RobotModel.StatusE.CONNECTED == status || RobotModel.StatusE.CONNECTING == status)
                {
                    return false;
                }
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
