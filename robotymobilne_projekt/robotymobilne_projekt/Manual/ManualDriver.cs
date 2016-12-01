using System;
using System.Threading;
using robotymobilne_projekt.Devices;
using robotymobilne_projekt.Settings;

namespace robotymobilne_projekt.Manual
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
            while (null != robot && null != controller && robot.Status != RemoteDevice.StatusE.DISCONNECTED)
            {
                try
                {
                    var reading = controller.execute();
                    robot.SpeedL = reading.Item1;
                    robot.SpeedR = reading.Item2;
                    var dataFrame = robot.calculateFrame();

                    if (null != dataFrame)
                    {
                        Robot.sendData(dataFrame);
                    }

                    if (!ControllerSettings.Instance.Controllers.Contains(controller))
                            throw new InvalidOperationException();
                }
                catch (Exception)
                {
                    break;
                }
                Thread.Sleep(ControllerSettings.Instance.Latency);
            }
            Dispose();
        }

        // Clean-up code
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
