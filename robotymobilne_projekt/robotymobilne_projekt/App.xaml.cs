using System.Windows;
using robotymobilne_projekt.GUI;
using robotymobilne_projekt.Settings;
using robotymobilne_projekt.Utils.AppLogger;

namespace robotymobilne_projekt
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            ControllerSettings.Instance.initialize();
            RobotSettings.Instance.initialize();
            Logger.getLogger().log(LogLevel.INFO, "Starting application...");
            mainWindow.Show();
        }
    }
}
