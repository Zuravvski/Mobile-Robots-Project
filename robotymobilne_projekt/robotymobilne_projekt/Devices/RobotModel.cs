using System;
using System.IO;
using System.Windows;
using robotymobilne_projekt.Devices.Network_utils;
using robotymobilne_projekt.Utils.AppLogger;

namespace robotymobilne_projekt.Devices
{
    public class RobotModel : RemoteDevice
    {
        // Robot data
        private Point position;
        private int battery;
        private double speedL;
        private double speedR;
        private bool isNotReserved;


        #region Setters & Getters
        public bool IsNotReserved
        {
            get
            {
                return isNotReserved;
            }
            set
            {
                isNotReserved = value;
                NotifyPropertyChanged("IsNotReserved");
            }
        }

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
                if (tcpClient.Connected && value >= 4300 && value <= 5000)
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
            isNotReserved = true;
        }

        public override string ToString()
        {
            return deviceName;
        }

        public override void sendData(string data)
        {
            try
            {
                if (Status != StatusE.CONNECTED) return;

                var oFrame = new RobotFrame(data);
                var frameToSend = oFrame.getFrame();
                networkStream.BeginWrite(frameToSend, 0, frameToSend.Length, sendCallback, tcpClient);
            }
            catch (IOException)
            {
                Status = StatusE.DISCONNECTED;
                Logger.Instance.log(LogLevel.INFO, string.Format("Device {0} disconnected. Connection terminated by host.", this));
            }
            catch
            {
                Logger.Instance.log(LogLevel.INFO, "Could not send data to device: " + deviceName);
            }
        }

        protected override void receiveData()
        {
            try
            {
                //if (status != StatusE.CONNECTED) return;

                var receiveBuffer = new byte[28];
                networkStream.BeginRead(receiveBuffer, 0, receiveBuffer.Length, receiveCallback, tcpClient);
                var oFrame = new RobotFrame(receiveBuffer);
                oFrame.parseFrame(this);
            }
            catch (IOException ex)
            {
                Logger.Instance.log(LogLevel.INFO, string.Format("Device {0} disconnected. Connection terminated by host.", this));
            }
            catch
            {
                disconnect();
                Logger.Instance.log(LogLevel.INFO, string.Format("Lost connection with {0}", this));
            }
        }
    }
}
