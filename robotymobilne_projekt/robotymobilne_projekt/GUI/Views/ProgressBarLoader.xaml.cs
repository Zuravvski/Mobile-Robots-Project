using System;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Windows.Controls;
using MahApps.Metro.Controls;
using robotymobilne_projekt.Settings;

namespace robotymobilne_projekt.GUI.Views
{
    /// <summary>
    /// Interaction logic for ProgressBarLoader.xaml
    /// </summary>
    public partial class ProgressBarLoader : ModernDialog
    {
        public ProgressBarLoader()
        {
            InitializeComponent();

            CloseButton.Name = "Abort";
            CloseButton.Content = "Abort";
            CloseButton.IsCancel = true;

            Task.Run(() => ApplicationService.Instance.handleModeChange(this, 
                ApplicationService.ApplicationMode.SERVER));
        }

        public void CloseWindow()
        {
            Dispatcher.BeginInvoke(new Action(Close));
        }
    }
}