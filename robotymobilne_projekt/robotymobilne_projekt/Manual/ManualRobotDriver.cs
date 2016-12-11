using System;
using System.Threading;
using robotymobilne_projekt.Devices;
using robotymobilne_projekt.Settings;

namespace robotymobilne_projekt.Manual
{
    public class ManualRobotDriver : RobotDriver
    {
        public ManualRobotDriver(RobotModel robot, AbstractController controller) : base(robot, controller)
        {

        }

        protected override void run()
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
                catch
                {
                    break;
                }
                Thread.Sleep(ControllerSettings.Instance.Latency);
            }
            Dispose();
        }
    }
}
