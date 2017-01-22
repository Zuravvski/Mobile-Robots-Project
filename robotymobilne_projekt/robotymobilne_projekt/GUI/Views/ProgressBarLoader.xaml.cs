using System;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Windows.Controls;
using robotymobilne_projekt.Settings;
using FirstFloor.ModernUI.Presentation;
using System.Windows.Media;

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

            progressRing.Foreground = new SolidColorBrush(AppearanceManager.Current.AccentColor);

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