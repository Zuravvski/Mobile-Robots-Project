using System;
using System.IO;
using robotymobilne_projekt.Utils;

namespace robotymobilne_projekt.Settings
{
    public class ApplicationSettings : ObservableObject
    {
        private static readonly Lazy<ApplicationSettings> instance = new Lazy<ApplicationSettings>(() => new ApplicationSettings());
        private string ip;
        private int port;
        private ApplicationMode appMode;
        private SendMode transMode;
        private StreamWriter fileWriter;

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
                appMode = value;
                NotifyPropertyChanged("AppMode");
            }
        }

        public SendMode TranssionMode
        {
            get
            {
                return transMode;
            }
            set
            {
                transMode = value;
                NotifyPropertyChanged("SendMode");
            }
        }

        public static ApplicationSettings Instance
        {
            get { return instance.Value; }
        }
        #endregion

        public ApplicationSettings()
        {
            appMode = ApplicationMode.DIRECT;
            ip = DEFAULT_SERVER_IP;
            port = DEFAULT_SERVER_PORT;
        }

        public enum ApplicationMode
        {
            DIRECT, SERVER
        }

        public enum SendMode
        {
            PACKET, RAW
        }


        public void save()
        {
            if (null == fileWriter)
            {
                fileWriter = new StreamWriter("AppSettings.txt");
                fileWriter.WriteLine(Ip);
                fileWriter.WriteLine(Port);
                fileWriter.Flush();
            }
        }
    }
}
