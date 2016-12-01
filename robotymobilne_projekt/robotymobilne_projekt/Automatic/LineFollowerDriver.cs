using System;
using System.Threading;
using robotymobilne_projekt.Devices;
using robotymobilne_projekt.Settings;

namespace robotymobilne_projekt.Automatic
{
    public class LineFollowerDriver : IDisposable
    {
        private RobotModel robot;
        private LineFollower controller;
        private readonly Thread handlerThread;

        public LineFollowerDriver(RobotModel robot, LineFollower controller)
        {
            this.robot = robot;
            this.controller = controller;
            handlerThread = new Thread(run) {IsBackground = true};
            handlerThread.Start();
        }

        private void run()
        {
            while (robot.Status != RemoteDevice.StatusE.DISCONNECTED)
            {
                try
                {
                    controller.Sensors = robot.Sensors;
                    var reading = controller.execute();
                    robot.SpeedL = reading.Item1;
                    robot.SpeedR = reading.Item2;
                    var dataFrame = robot.calculateFrame();

                    if (null != dataFrame)
                    {
                        robot.sendData(dataFrame);
                    }

                    Thread.Sleep(ControllerSettings.Instance.Latency);
                }
                catch
                {
                    break;
                }
            }
            Dispose();
        }

        public void Dispose()
        {
            controller = null;
            robot.Status = RemoteDevice.StatusE.DISCONNECTED;
            robot.disconnect();
            robot = null;
            handlerThread.Abort();
        }
    }
}
