using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using robotymobilne_projekt.Settings;

namespace robotymobilne_projekt.GUI.Converters
{
    public class AppModeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = Visibility.Visible;
            if (value is ApplicationService.ApplicationMode)
            {
                var mode = (ApplicationService.ApplicationMode)value;

                switch (mode)
                {
                      case  ApplicationService.ApplicationMode.DIRECT:
                        result = Visibility.Visible;
                        break;

                    case ApplicationService.ApplicationMode.SERVER:
                        result = Visibility.Collapsed;
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
