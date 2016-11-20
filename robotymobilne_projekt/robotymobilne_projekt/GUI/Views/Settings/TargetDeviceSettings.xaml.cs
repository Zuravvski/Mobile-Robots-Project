using robotymobilne_projekt.GUI.Views.Settings;
using System.Windows.Controls;

namespace MobileRobots.GUI.Views.Settings
{
    /// <summary>
    /// Interaction logic for TargetDeviceSettings.xaml
    /// </summary>
    public partial class TargetDeviceSettings : UserControl
    {
        public TargetDeviceSettings()
        {
            InitializeComponent();
            DataContext = new RobotSettingsViewModel();
        }
    }
}
