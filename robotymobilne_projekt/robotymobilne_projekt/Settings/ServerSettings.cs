using System;
using robotymobilne_projekt.Utils;

namespace robotymobilne_projekt.Settings
{
    public class ServerSettings : ObservableObject
    {
        private static Lazy<ServerSettings> instance = new Lazy<ServerSettings>(() => new ServerSettings());
        private string ip;
        private int port;

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

        public static ServerSettings Instance
        {
            get { return instance.Value; }
        }
        #endregion
    }
}
