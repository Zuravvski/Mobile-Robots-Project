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
            if(value is RemoteDevice.StatusE)
            {
                RemoteDevice.StatusE status = (RemoteDevice.StatusE)value;
                if(RemoteDevice.StatusE.CONNECTED == status || RemoteDevice.StatusE.CONNECTING == status)
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
