using System.Windows.Controls;
using robotymobilne_projekt.GUI.ViewModels;

namespace robotymobilne_projekt.GUI.Views.Settings
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
