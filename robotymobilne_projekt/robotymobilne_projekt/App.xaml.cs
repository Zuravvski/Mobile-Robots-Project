using MobileRobots.Utils.AppLogger;
using robotymobilne_projekt.Settings;
using robotymobilne_projekt.Utils.AppLogger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MobileRobots
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
