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
using robotymobilne_projekt.Automatic;
using robotymobilne_projekt.Devices;
using robotymobilne_projekt.GUI.ViewModels;
using robotymobilne_projekt.Settings;

namespace robotymobilne_projekt.GUI.Views.Automatic
{
    /// <summary>
    /// Interaction logic for LineFollowerView.xaml
    /// </summary>
    public partial class LineFollowerView : UserControl
    {
        public LineFollowerView()
        {
            // TODO: Delete hardcode
            RobotSettings.Instance.Robots[1].connect();
            DataContext = new LineFollowerViewModel(RobotSettings.Instance.Robots[1], new LineFollower());
        }
    }
}
