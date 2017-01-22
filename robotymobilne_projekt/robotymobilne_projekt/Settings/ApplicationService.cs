using System;
using System.IO;
using System.Threading.Tasks;
using robotymobilne_projekt.Devices;
using robotymobilne_projekt.GUI.Views;
using robotymobilne_projekt.Network;
using robotymobilne_projekt.Utils;

namespace robotymobilne_projekt.Settings
{
    public class ApplicationService : ObservableObject
    {
        private static readonly Lazy<ApplicationService> instance = new Lazy<ApplicationService>(() => new ApplicationService());
        private string ip;
        private int port;
        private ApplicationMode appMode;
        private ServerService serverService;
        private readonly RobotSettings robotSettings;

        #region Constants
        public static readonly string DEFAULT_SERVER_IP = "127.0.0.1";
        public static readonly int DEFAULT_SERVER_PORT = 50131;
        #endregion

        #region Setters & Getters

        public string Ip
        {
            get { return ip; }
            set
            {
                ip = value;
                NotifyPropertyChanged("Ip");
            }
        }

        public int Port
        {
            get { return port; }
            set
            {
                port = value;
                NotifyPropertyChanged("Port");
            }
        }

        public ApplicationMode AppMode
        {
            get { return appMode; }
            private set
            {
                appMode = value;
                NotifyPropertyChanged("AppMode");
            }
        }

        public static ApplicationService Instance
        {
            get { return instance.Value; }
        }
        #endregion

        public ApplicationService()
        {
            robotSettings = RobotSettings.Instance;
            appMode = ApplicationMode.DIRECT;
            loadServerSettings();
        }

        private void loadServerSettings()
        {
            try
            {
                var loadSettings = new StreamReader("Settings.txt");
                ip = loadSettings.ReadLine();
                port = Convert.ToInt32(loadSettings.ReadLine());
            }
            catch
            {
                ip = DEFAULT_SERVER_IP;
                port = DEFAULT_SERVER_PORT;
            }
        }

        public enum ApplicationMode
        {
            DIRECT, SERVER, SWITCHING
        }


        public void handleModeChange(ProgressBarLoader loader, ApplicationMode mode)
        {
            if(appMode == mode) return;

            foreach (var robot in robotSettings.Robots)
            {
                if ((robot is NullObjectRobot)) continue;

                robot.disconnect();

                if (ApplicationMode.DIRECT == mode)
                {
                    serverService?.Dispose();
                    serverService = null;
                    robot.Mode = new DirectMode(robot);
                    AppMode = ApplicationMode.DIRECT;
                }
                else
                {
                    if (null == serverService)
                    {
                        serverService = new ServerService();
                        serverService.connect();
                    }
                    var serverMode = new ServerMode(serverService);
                    robot.Mode = serverMode;
                    serverMode.Robot = robot;
                    AppMode = ApplicationMode.SERVER;
                }
                loader.CloseWindow();
            }
        }

        public void save()
        {
            if (!Directory.Exists("Logs"))
            {
                Directory.CreateDirectory("Logs");
            }
            var fileWriter = new StreamWriter("Settings.txt");
            fileWriter.WriteLine(Ip);
            fileWriter.WriteLine(Port);
            fileWriter.Flush();
        }
    }
}
