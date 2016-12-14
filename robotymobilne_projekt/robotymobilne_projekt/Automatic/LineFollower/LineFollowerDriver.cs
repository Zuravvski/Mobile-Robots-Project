using System.Threading;
using robotymobilne_projekt.Devices;
using robotymobilne_projekt.Settings;

namespace robotymobilne_projekt.Automatic.LineFollower
{
    public class LineFollowerDriver : RobotDriver
    {
        public LineFollowerDriver(RobotModel robot, LineFollowerController controller) : base(robot, controller)
        {

        }

        protected override void run()
        {
            while (null != robot && null != controller && robot.Status != RemoteDevice.StatusE.DISCONNECTED)
            {
                try
                {
                    ((LineFollowerController)controller).Sensors = robot.Sensors;
                    var reading = controller.execute();
                    robot.SpeedL = reading.Item1;
                    robot.SpeedR = reading.Item2;
                    var dataFrame = robot.calculateFrame();

                    if (null != dataFrame)
                    {
                        Robot.sendData(dataFrame);
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
    }
}
