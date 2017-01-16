using System;
using System.Globalization;
using System.Windows.Data;
using robotymobilne_projekt.Devices;

namespace robotymobilne_projekt.GUI.Converters
{
    public class RobotStatusToTextConverter : IValueConverter
    {
        private const string connected = "Connected";
        private const string connecting = "Connecting";
        private const string disconnected = "Disconnected";
        private const string unknownStatus = "Unknown";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is RobotModel.StatusE)
            {
                var status = (RobotModel.StatusE)value;
                switch (status)
                {
                    case RobotModel.StatusE.CONNECTED:
                        return connected;

                    case RobotModel.StatusE.CONNECTING:
                        return connecting;

                    default:
                        return disconnected;
                }
            }
            return unknownStatus;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch(value.ToString().ToLower())
            {
                case connected:
                    return RobotModel.StatusE.CONNECTED;

                case connecting:
                    return RobotModel.StatusE.CONNECTING;

                default:
                    return RobotModel.StatusE.DISCONNECTED;
            }
        }
    }
}
