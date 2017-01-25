using System;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Windows;
using robotymobilne_projekt.Network;
using robotymobilne_projekt.Settings;
using robotymobilne_projekt.Utils;

namespace robotymobilne_projekt.Devices
{
    [Serializable]
    public class RobotModel : ObservableObject
    {
        // Robot data
        private int x;
        private int y;
        private int angle;
        private int battery;
        private double speedL;
        private double speedR;
        private ObservableCollection<int> sensors;

        // Networking stuff
        private bool isNotReserved;
        private TcpClient socket;
        private StatusE status;
        private IConnectionMode connectionManager;

        #region Setters & Getters

        public IConnectionMode Mode
        {
            get { return connectionManager; }
            set
            {
                connectionManager = value;
                NotifyPropertyChanged("Mode");
            }
        }

        public int ID { get; }

        public TcpClient Socket
        {
            get { return socket; }
            set { socket = value; }
        }

        public bool Connected => null != socket && socket.Connected;

        public bool IsNotReserved
        {
            get { return isNotReserved; }
            set
            {
                isNotReserved = value;
                NotifyPropertyChanged("IsNotReserved");
            }
        }

        public int X
        {
            get { return x; }
            set
            {
                x = value;
            }
        }

        public int Y
        {
            get { return y; }
            set
            {
                y = value;
            }
        }

        public int Angle
        {
            get { return angle; }
            set
            {
                angle = value;
            }
        }

        public int Battery
        {
            get { return battery; }
            set
            {
                if (null != socket && status == StatusE.CONNECTED && value >= 4300 && value <= 5000)
                {
                    battery = value;
                }
                else
                {
                    battery = 4300;
                }
                NotifyPropertyChanged("Battery");
            }
        }

        public double SpeedL
        {
            get { return speedL; }
            set
            {
                if (status == StatusE.CONNECTED)
                {
                    if (value > 127)
                    {
                        speedL = 127;
                    }
                    else if (value < -127)
                    {
                        speedL = -127;
                    }
                    else
                    {
                        speedL = value;
                    }
                }
                else
                {
                    speedL = 0;
                }
                NotifyPropertyChanged("SpeedL");
            }
        }

        public double SpeedR
        {
            get { return speedR; }
            set
            {
                if (status == StatusE.CONNECTED)
                {
                    if (value > 127)
                    {
                        speedR = 127;
                    }
                    else if (value < -127)
                    {
                        speedR = -127;
                    }
                    else
                    {
                        speedR = value;
                    }
                }
                else
                {
                    speedR = 0;
                }
                NotifyPropertyChanged("SpeedR");
            }
        }

        public ObservableCollection<int> Sensors
        {
            get { return sensors; }
            set
            {
                sensors = value;
                NotifyPropertyChanged("Sensors");
            }
        }

        public StatusE Status
        {
            get { return status; }
            set
            {
                status = value;
                NotifyPropertyChanged("Status");
            }
        }

        public string IP { get; }
        public string Name { get; }
        public int Port { get; }

        #endregion

        public RobotModel(int id, string ip, int port)
        {
            ID = id;
            isNotReserved = true;
            IP = ip;
            Port = port;
            Status = StatusE.DISCONNECTED;
            socket = new TcpClient(AddressFamily.InterNetwork);
            sensors = new ObservableCollection<int>() {0, 0, 0, 0, 0};
            connectionManager = new DirectMode(this);
        }

        public override string ToString()
        {
            return "ID: " + Convert.ToString(ID);
        }

        public virtual void connect()
        {
            connectionManager.connect();
        }

        public virtual void sendData(Packet data)
        {
            connectionManager.send(data);
        }

        public virtual void disconnect()
        {
            connectionManager.disconnect();
            cleanUpModel();
        }

        private void cleanUpModel()
        {
            SpeedL = 0;
            SpeedR = 0;
            Battery = 0;
            X = 0;
            Y = 0;
            Angle = 0;
            Sensors = new ObservableCollection<int>() {0,0,0,0,0};
            Status = StatusE.DISCONNECTED;
        }

        public Packet calculateFrame()
        {
            string frameLights = RobotSettings.noLights, frameL, frameR;

            if (RobotSettings.Instance.UseLights)
            {
                if (SpeedL == SpeedR)
                {
                    frameLights = RobotSettings.bothLights;
                }
                if (SpeedR > SpeedL)
                {
                    frameLights = RobotSettings.rightLight;
                }
                if (SpeedL > SpeedR)
                {
                    frameLights = RobotSettings.leftLight;
                }
            }

            if (SpeedL >= 0)
            {
                frameL = ((int)SpeedL).ToString("X2");
            }
            else
            {
                frameL = ((int)SpeedL).ToString("X2").Substring(((int)SpeedL).ToString("X2").Length - 2);
            }

            if (SpeedR >= 0)
            {
                frameR = ((int)SpeedR).ToString("X2");
            }
            else
            {
                frameR = ((int)SpeedR).ToString("X2").Substring(((int)SpeedR).ToString("X2").Length - 2);
            }
            var finalFrame = frameLights + frameL + frameR;

            return new Packet(PacketHeader.UNRECOGNISED, finalFrame);
        }

        public enum StatusE
        {
            CONNECTED,
            DISCONNECTED,
            CONNECTING
        };
    }
}
