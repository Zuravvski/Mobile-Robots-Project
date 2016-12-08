using System;
using System.Globalization;
using System.Windows.Data;
using robotymobilne_projekt.GUI.Views.Automatic;

namespace robotymobilne_projekt.GUI.Converters
{
    public class AutoModeToStringConverter : IValueConverter
    {
        private const string None = "NONE";
        private const string LineFollower = "Line follower";
        private const string RoadTracking = "Road tracking";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = None;
            if (value is AutomaticMode)
            {
                var mode = (AutomaticMode) value;
                switch (mode)
                {
                    case AutomaticMode.LINE_FOLLOWER:
                        result = LineFollower;
                        break;

                    case AutomaticMode.ROAD_TRACKING:
                        result = RoadTracking;
                        break;
                }
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = AutomaticMode.NONE;
            if (value is string)
            {
                var mode = (string) value;
                switch (mode)
                {
                    case LineFollower:
                        result = AutomaticMode.LINE_FOLLOWER;
                        break;

                    case RoadTracking:
                        result = AutomaticMode.ROAD_TRACKING;
                        break;
                }
            }
            return result;
        }
    }
}
