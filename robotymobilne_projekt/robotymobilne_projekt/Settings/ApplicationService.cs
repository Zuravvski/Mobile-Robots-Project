using System;
using robotymobilne_projekt.Devices;
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
            set
            {
                if (value != appMode)
                {
                    appMode = value;
                    handleModeChange();
                }
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
            ip = DEFAULT_SERVER_IP;
            port = DEFAULT_SERVER_PORT;
        }

        public enum ApplicationMode
        {
            DIRECT, SERVER, SWITCHING
        }


        private void handleModeChange()
        {
            foreach (var robot in robotSettings.Robots)
            {
                if((robot is NullObjectRobot)) continue;

                robot.disconnect();

                if (appMode == ApplicationMode.DIRECT)
                {
                    serverService?.Dispose();
                    serverService = null;
                    robot.Mode = new DirectMode(robot);
                }
                else
                {
                    if (null == serverService)
                    {
                        serverService = new ServerService();
                        serverService.connect();
                    }
                    var serverMode = new ServerMode(serverService.Socket);
                    robot.Mode = serverMode;
                    serverMode.Robot = robot;
                }
            }
        }
    }
}
