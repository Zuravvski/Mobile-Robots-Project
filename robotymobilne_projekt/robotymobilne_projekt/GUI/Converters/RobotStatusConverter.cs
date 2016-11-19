using MobileRobots;
using System;
using System.Globalization;
using System.Windows.Data;

namespace robotymobilne_projekt.GUI.Converters
{
    class RobotStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is RobotModel.statusE)
            {
                RobotModel.statusE status = (RobotModel.statusE)value;
                switch (status)
                {
                    case RobotModel.statusE.CONNECTED:
                        return "Connected";

                    case RobotModel.statusE.DISCONNECTED:
                    default:
                        return "Disconnected";
                }
            }
            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch(value.ToString().ToLower())
            {
                case "connected":
                    return RobotModel.statusE.CONNECTED;

                case "disconnected":
                default:
                    return RobotModel.statusE.DISCONNECTED;
            }
        }
    }
}
