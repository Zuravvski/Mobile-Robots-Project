using System;
using System.IO;
using System.Threading;
using System.Windows;
using robotymobilne_projekt.Devices.Network_utils;
using robotymobilne_projekt.Settings;
using robotymobilne_projekt.Utils.AppLogger;

namespace robotymobilne_projekt.Devices
{
    [Serializable]
    public class RobotModel : RemoteDevice
    {
        // Robot data
        private Point position;
        private int battery;
        private double speedL;
        private double speedR;
        private bool isNotReserved;
        private int[] sensors;

        private readonly RobotFrameParser robotFrameParser;

        #region Setters & Getters

        public bool IsNotReserved
        {
            get { return isNotReserved; }
            set
            {
                isNotReserved = value;
                NotifyPropertyChanged("IsNotReserved");
            }
        }

        public Point Position
        {
            get { return position; }
            set
            {
                position = value;
                NotifyPropertyChanged("Position");
            }
        }

        public int Battery
        {
            get { return battery; }
            set
            {
                if (null != tcpClient && tcpClient.Connected && value >= 4300 && value <= 5000)
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
                if (null != tcpClient && tcpClient.Connected)
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
                if (null != tcpClient && tcpClient.Connected)
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

        public int[] Sensors
        {
            get { return sensors; }
            set { sensors = value; }
        }

        #endregion

        public RobotModel(string name, string ip, int port) : base(name, ip, port)
        {
            isNotReserved = true;
            sensors = new[] {0, 0, 0, 0, 0};
            robotFrameParser = new RobotFrameParser(this);
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

                var frameToSend = new RobotFrame(data).getFrame();
                networkStream.BeginWrite(frameToSend, 0, frameToSend.Length, sendCallback, tcpClient);
            }
            catch (IOException)
            {
                disconnect();
                Logger.Instance.log(LogLevel.INFO,
                    string.Format("Device {0} disconnected. Connection terminated by host.", this));
            }
            catch
            {
                Logger.Instance.log(LogLevel.INFO, "Could not send data to device: " + deviceName);
            }
        }

        protected override void sendCallback(IAsyncResult result)
        {
            try
            {
                networkStream.EndWrite(result);
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
                var receiveBuffer = new byte[1024];
                networkStream.BeginRead(receiveBuffer, 0, receiveBuffer.Length, receiveCallback, receiveBuffer);
            }
            catch (IOException)
            {
                disconnect();
                Logger.Instance.log(LogLevel.INFO,
                    string.Format("Device {0} disconnected. Connection terminated by host.", this));
            }
            catch
            {
                Logger.Instance.log(LogLevel.INFO, string.Format("Could not retreive data from {0}", this));
            }
        }

        protected override void receiveCallback(IAsyncResult result)
        {
            try
            {
                var receiveBuffer = (byte[]) result.AsyncState;
                var bytesRead = networkStream.EndRead(result);  
                Array.Resize(ref receiveBuffer, bytesRead);

                if (RobotFrameParser.FRAME_SIZE == receiveBuffer.Length)
                {
                    robotFrameParser.parse(receiveBuffer);
                }

                Thread.Sleep(ControllerSettings.Instance.Latency); // to provide equal send-receive ratio
                receiveData();
            }
            catch (Exception)
            {
                Logger.Instance.log(LogLevel.INFO, string.Format("Could not retreive data from {0}", this));
            }
        }

        public string calculateFrame()
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

            return finalFrame;
        }

        public override void disconnect()
        {
            base.disconnect();
            cleanUpModel();
        }

        private void cleanUpModel()
        {
            SpeedL = 0;
            SpeedR = 0;
            Battery = 0;
            Position = new Point(0,0);
        }
    }
}
