using MobileRobots.Manual;
using MobileRobots.Utils;
using robotymobilne_projekt.Devices.Network;
using System;
using System.Threading;
using System.Windows;

namespace MobileRobots
{
    class RobotModel : RemoteDevice
    {
        // Robot data
        private Point position;
        private int speed;
        private int battery;
        private int status;
        private MOVE_DIRECTION direction;

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
        public int SPEED
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
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
        public MOVE_DIRECTION DIRECTION
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
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

        public RobotModel(string name, string ip, int port) : base(name, ip, port)
        {
            direction = MOVE_DIRECTION.IDLE;
            controllerThread = new Thread(handleController);
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
                Thread.Sleep(controller.LATENCY);
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

                Logger.getLogger().log(string.Format("Battery state from {0}: {1}mV", this, battery.ToString()));
                Logger.getLogger().log(string.Format("Robot {0} status is: {1}", this, status.ToString()));
            }
            catch (Exception)
            {
                Logger.getLogger().log(string.Format("Lost connection with {0}", this));
            }
        }

        public enum MOVE_DIRECTION { IDLE, FORWARD, BACKWARD };
    }
}
