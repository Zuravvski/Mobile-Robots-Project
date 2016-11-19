using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MobileRobots.Pages.Settings
{
    /// <summary>
    /// Interaction logic for TargetDeviceSettings.xaml
    /// </summary>
    public partial class TargetDeviceSettings : UserControl
    {
        double defaultSpeed;
        double defaultNitro;
        double defaultSteer;

        public TargetDeviceSettings()
        {
            InitializeComponent();
            defaultSpeed = slider_maxSpeed.Value;
            defaultNitro = slider_nitroFactor.Value;
            defaultSteer = slider_steeringRatio.Value;
              
        }

        private void button_deviceReset_Click(object sender, RoutedEventArgs e)
        {
            resetGui();
        }

        private void button_deviceApply_Click(object sender, RoutedEventArgs e)
        {
            applySettings();
        }


        private void applySettings()
        {

        }

        private void resetGui()
        {
            dynamic msgbox = MessageBox.Show("Do you really want to restore default settings? \nThis option cannot be undone!", "Please confirm", MessageBoxButton.YesNo);

            if (msgbox == MessageBoxResult.Yes)
            {
                radio_lightsOn.IsChecked = true;
                slider_maxSpeed.Value = defaultSpeed;
                slider_nitroFactor.Value = defaultNitro;
                slider_steeringRatio.Value = defaultSteer;
            }
        }
    }
}
