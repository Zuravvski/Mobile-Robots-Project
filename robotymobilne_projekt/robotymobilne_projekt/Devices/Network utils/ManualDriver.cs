using robotymobilne_projekt.Settings;
using System;
using System.Threading;
using OpenTK.Input;
using robotymobilne_projekt.Manual;

namespace robotymobilne_projekt.Devices.Network_utils
{
    public class ManualDriver : IDisposable
    {
        private RobotModel robot;
        private AbstractController controller;
        private readonly Thread handlerThread;

        public RobotModel Robot
        {
            get { return robot; }
        }

        public ManualDriver(RobotModel robot, AbstractController controller)
        {
            this.robot = robot;
            this.controller = controller;

            this.robot.IsNotReserved = false;
            this.controller.IsNotReserved = false;

            handlerThread = new Thread(run);
            handlerThread.Start();
        }

        private void run()
        {
            while (Robot.Status == RemoteDevice.StatusE.CONNECTED || Robot.Status == RemoteDevice.StatusE.CONNECTING)
            {
                try
                {
                    var reading = controller.execute();
                    robot.SpeedL = reading.Item1;
                    robot.SpeedR = reading.Item2;
                    var dataFrame = calculateFrame();

                    if (null != dataFrame)
                    {
                        Robot.sendData("[" + dataFrame + "]");
                    }
                }
                catch (Exception)
                {
                    break;
                }
                Thread.Sleep(ControllerSettings.Instance.Latency);
            }
            Dispose();
        }

        private string calculateFrame()
        {
            string frameLights = RobotSettings.noLights, frameL, frameR;

            if (RobotSettings.Instance.UseLights)
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

        public void Dispose()
        {
            robot.IsNotReserved = true;
            controller.IsNotReserved = true;
            robot.Status = RemoteDevice.StatusE.DISCONNECTED;
            robot.disconnect();
            robot = null;
            controller = null;
            handlerThread.Abort();
        }
    }
}
