using MobileRobots.Manual;
using MobileRobots.Utils;
using robotymobilne_projekt.Devices.Network;
using robotymobilne_projekt.Utils;
using System;
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
        private double maxSpeed;
        private double steeringSensivity;
        private double nitroValue;
        private double speedL;
        private double speedR;

        // Utilities
        private AbstractController controller;
        private Thread controllerThread;
        private ManualResetEvent hasController;

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
        public double MAXSPEED
        {
            get
            {
                return maxSpeed;
            }
            set
            {
                maxSpeed = value;
            }
        }
        public double STEERING_SENSIVITY
        {
            get
            {
                return steeringSensivity;
            }
            set
            {
                steeringSensivity = value;
            }
        }
        public double NITRO_VALUE
        {
            get
            {
                return nitroValue;
            }
            set
            {
                nitroValue = value;
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
            hasController = new ManualResetEvent(false);

            //default steering values
            maxSpeed = 50;
            steeringSensivity = 35;
            nitroValue = 1.2;
        }

        public override string ToString()
        {
            return "ID: " + deviceName;
        }

        public void handleController()
        {
            while (true)
            {
                hasController.WaitOne();
                string dataFrame = controller.execute();
                if (null != dataFrame && tcpClient.Connected)
                {
                    sendData(dataFrame);
                }
                Thread.Sleep(controller.LATENCY);
            }
        }

        public void run()
        {
            controllerThread.Start();
            hasController.Set();
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
