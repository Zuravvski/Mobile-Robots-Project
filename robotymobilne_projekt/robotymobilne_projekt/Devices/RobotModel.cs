using MobileRobots.Manual;
using MobileRobots.Utils.AppLogger;
using robotymobilne_projekt.Devices.Network;
using robotymobilne_projekt.Utils.AppLogger;
using System;
using System.Windows;

namespace MobileRobots
{
    public class RobotModel : RemoteDevice
    {
        // Robot data
        private Point position;
        private int battery;
        private double speedL;
        private double speedR;

        #region Setters & Getters
        public Point Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                NotifyPropertyChanged("Position");
            }
        }
        public int Battery
        {
            get
            {
                return battery;
            }
            set
            {
                if (tcpClient.Connected)
                {
                    battery = value;
                }
                NotifyPropertyChanged("Battery");
            }
        }
        public double SpeedL
        {
            get
            {
                return speedL;
            }
            set
            {
                if (tcpClient.Connected)
                {
                    if (value > 127)
                    {
                        speedL = 127;
                    }
                    else if(value < -127)
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
            get
            {
                return speedR;
            }
            set
            {
                if (tcpClient.Connected)
                {
                    if (value > 127)
                    {
                        speedR = 127;
                    }
                    else if(value < -127)
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
        #endregion

        public RobotModel(string name, string ip, int port) : base(name, ip, port)
        {
            Battery = 4400;
        }

        public override string ToString()
        {
            return deviceName;
        }
    
        protected override void receiveData()
        {
            try
            {
                byte[] receiveBuffer = new byte[28];
                networkStream.BeginRead(receiveBuffer, 0, receiveBuffer.Length, new AsyncCallback(receiveCallback), tcpClient);
                //RobotFrame oFrame = new RobotFrame(receiveBuffer);
                //oFrame.parseFrame(this);
                Battery = new Random().Next(4300, 5000);

                Logger.getLogger().log(LogLevel.INFO, string.Format("Robot {0} status is: {1}", this, status.ToString()));
            }
            catch (Exception)
            {
                Logger.getLogger().log(LogLevel.WARNING, string.Format("Lost connection with {0}", this));
            }
        }
    }
}
