using robotymobilne_projekt.Utils.AppLogger;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using robotymobilne_projekt.GUI.ViewModels.Automatic;

namespace robotymobilne_projekt.GUI.Views.Automatic
{
    /// <summary>
    /// Interaction logic for LineFollowerView.xaml
    /// </summary>
    public partial class AutomaticView : UserControl
    {
        public AutomaticView()
        {
            DataContext = new AutomaticViewModel(this);
        }
    }
}
