using MobileRobots.Manual;
using MobileRobots.Utils.AppLogger;
using robotymobilne_projekt.Devices.Network;
using robotymobilne_projekt.Settings;
using robotymobilne_projekt.Utils.AppLogger;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace MobileRobots
{
    class RobotModel : RemoteDevice
    {
        // Robot data
        private Point position;
        private int battery;
        private int status;
        private double speedL;
        private double speedR;
        private RobotSettings robotSettings;

        // Utilities
        private AbstractController controller;
        private Thread controllerThread;

        // Setters and getters
        public Point POSITION
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }
        public int BATTERY
        {
            get
            {
                return battery;
            }
            set
            {
                battery = value;
            }
        }
        public AbstractController CONTROLLER
        {
            get
            {
                return controller;
            }
            set
            {
                controller = value;
            }
        }
        public int STATUS
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }
        public double SPEED_L
        {
            get
            {
                return speedL;
            }
            set
            {
                if (value > 127)
                {
                    speedL = 127;
                }
                else
                {
                    speedL = 127;
                }
            }
        }
        public double SPEED_R
        {
            get
            {
                return speedR;
            }
            set
            {
                if (value > 127)
                {
                    speedR = 127;
                }
                else
                {
                    speedL = value;
                }
            }
        }

        public RobotModel(string name, string ip, int port) : base(name, ip, port)
        {
            controllerThread = new Thread(handleController);
            robotSettings = RobotSettings.INSTANCE;
        }

        public override string ToString()
        {
            return "ID: " + deviceName;
        }

        public void handleController()
        {
            while (true)
            {
                string dataFrame = controller.execute();
                if (null != dataFrame && tcpClient.Connected)
                {
                    sendData(dataFrame);
                }
                Thread.Sleep(ControllerSettings.INSTANCE.LATENCY);
            }
        }

        public void run()
        {
            controllerThread.Start();
        }

        protected override void receiveData()
        {
            try
            {
                byte[] receiveBuffer = new byte[28];
                networkStream.BeginRead(receiveBuffer, 0, receiveBuffer.Length, new AsyncCallback(receiveCallback), tcpClient);
                RobotFrame oFrame = new RobotFrame(receiveBuffer);
                oFrame.parseFrame(this);

                Logger.getLogger().log(LogLevel.INFO, string.Format("Battery state from {0}: {1}mV", this, battery.ToString()));
                Logger.getLogger().log(LogLevel.INFO, string.Format("Robot {0} status is: {1}", this, status.ToString()));
            }
            catch (Exception)
            {
                Logger.getLogger().log(LogLevel.WARNING, string.Format("Lost connection with {0}", this));
            }
        }
    }
}
