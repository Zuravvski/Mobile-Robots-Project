using MobileRobots;
using MobileRobots.Manual;
using robotymobilne_projekt.Settings;
using System;
using System.Threading;

namespace robotymobilne_projekt.Devices.Network_utils
{
    public class ManualDriver
    {
        private RobotModel robot;
        private AbstractController controller;
        private Thread handlerThread;

        public RobotModel Robot
        {
            get
            {
                return robot;
            }
        }

        public ManualDriver(RobotModel robot, AbstractController controller)
        {
            this.robot = robot;
            this.controller = controller;

            ControllerSettings.Instance.reserveController(controller);
            RobotSettings.Instance.reserveRobot(robot);
            ControllerSettings.Instance.notifyObservers();

            handlerThread = new Thread(run);
            handlerThread.Start();
        }

        private void run()
        {
            while(Robot.Status == RemoteDevice.StatusE.CONNECTED || Robot.Status == RemoteDevice.StatusE.CONNECTING)
            {
                string dataFrame = controller.execute();

                if (null != dataFrame && dataFrame.Length == 6)
                {
                    RobotTransmitFrame transmitFrame = new RobotTransmitFrame(dataFrame);
                    transmitFrame.parseFrame(Robot);
                    Robot.sendData(dataFrame);
                }
                Thread.Sleep(ControllerSettings.Instance.Latency);
            }

            RobotSettings.Instance.freeRobot(robot);
            ControllerSettings.Instance.freeController(controller);
            robot.disconnect();
        }
    }
}
