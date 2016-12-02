using robotymobilne_projekt.Utils.AppLogger;
using System;
using System.Diagnostics;
using System.Windows.Controls;

namespace robotymobilne_projekt.GUI.Views.Automatic
{
    /// <summary>
    /// Interaction logic for LineFollowerView.xaml
    /// </summary>
    public partial class AutomaticView : UserControl
    {
        public AutomaticView()
        {
            InitializeComponent();
            viewboxMapArea.Child = new LineFollowerView();
        }
    }
}
