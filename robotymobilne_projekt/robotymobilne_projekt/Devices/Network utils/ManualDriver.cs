using robotymobilne_projekt.Settings;
using System;
using System.Threading;
using robotymobilne_projekt.Manual;

namespace robotymobilne_projekt.Devices.Network_utils
{
    public class ManualDriver
    {
        private RobotModel robot;
        private AbstractController controller;
        private Thread handlerThread;

        // Settings instances
        private RobotSettings robotSettings = RobotSettings.Instance;

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
                Tuple<double, double> reading = controller.execute();
                robot.SpeedL = reading.Item1;
                robot.SpeedR = reading.Item2;
                string dataFrame = calculateFrame();

                if (null != dataFrame)
                {
                    Robot.sendData("["+ dataFrame + "]");
                }
                Thread.Sleep(ControllerSettings.Instance.Latency);
            }

            RobotSettings.Instance.freeRobot(robot);
            ControllerSettings.Instance.freeController(controller);
            robot.disconnect();
        }

        private string calculateFrame()
        {
            string frameLights = RobotSettings.noLights, frameL, frameR;

            if (robotSettings.UseLights)
            {
                if (robot.SpeedL == robot.SpeedR)
                {
                    frameLights = RobotSettings.bothLights;
                }
                if (robot.SpeedR > robot.SpeedL)
                {
                    frameLights = RobotSettings.rightLight;
                }
                if (robot.SpeedL > robot.SpeedR)
                {
                    frameLights = RobotSettings.leftLight;
                }
            }

            if (robot.SpeedL >= 0)
            {
                frameL = ((int)robot.SpeedL).ToString("X2");
            }
            else
            {
                frameL = ((int)robot.SpeedL).ToString("X2").Substring(((int)robot.SpeedL).ToString("X2").Length - 2);
            }

            if (robot.SpeedR >= 0)
            {
                frameR = ((int)robot.SpeedR).ToString("X2");
            }
            else
            {
                frameR = ((int)robot.SpeedR).ToString("X2").Substring(((int)robot.SpeedR).ToString("X2").Length - 2);
            }
            var finalFrame = frameLights + frameL + frameR;

            return finalFrame;
        }
    }
}
