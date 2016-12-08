using robotymobilne_projekt.GUI.Views.Automatic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace robotymobilne_projekt.GUI.Converters
{
    class AutoModeCollToStringColl : IValueConverter
    {
        private const string none = "NONE";
        private const string lineFollower = "Line follower";
        private const string roadTracking = "Road tracking";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = new List<string>() {none};
            if (!(value is IEnumerable<AutomaticMode>)) return result;

            var modes = (List<AutomaticMode>) value;
            foreach (AutomaticMode mode in modes)
            {
                switch (mode)
                {
                    case AutomaticMode.LINE_FOLLOWER:
                    {
                        result.Add(lineFollower);
                        break;
                    }
                    case AutomaticMode.ROAD_TRACKING:
                    {
                        result.Add(roadTracking);
                        break;
                    }
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
