using robotymobilne_projekt.GUI.Views.Automatic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace robotymobilne_projekt.GUI.Converters
{
    class EnumToStringConverter : IValueConverter
    {
        private const string lineFollower = "Line follower";
        private const string Invalid = "Invalid";
        private const string roadTracking = "Road tracking";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = Invalid;
            if (value is AutomaticMode)
            {
                AutomaticMode mode = (AutomaticMode)value;
                switch (mode)
                {
                    case AutomaticMode.LINE_FOLLOWER:
                        {
                            result = lineFollower;
                            break;
                        }
                    case AutomaticMode.TRAJECTORY:
                        {
                            result = roadTracking;
                            break;
                        }
                }
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case lineFollower:
                    return AutomaticMode.LINE_FOLLOWER;

                case roadTracking:
                    return AutomaticMode.TRAJECTORY;

                default:
                    return AutomaticMode.INVALID;
            }
        }
    }
}
