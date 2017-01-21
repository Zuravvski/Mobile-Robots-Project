using System;
using System.Threading;
using robotymobilne_projekt.Devices;
using robotymobilne_projekt.Network;
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
            while (null != robot && null != controller && robot.Status != RobotModel.StatusE.DISCONNECTED)
            {
                try
                {
                    var reading = controller.execute();
                    robot.SpeedL = reading.Item1;
                    robot.SpeedR = reading.Item2;

                    Packet dataFrame;
                    if (ApplicationService.Instance.AppMode == ApplicationService.ApplicationMode.DIRECT)
                    {
                        dataFrame = robot.calculateFrame();
                    }
                    else
                    {
                        var velocity = robot.ID.ToString("00") + robot.calculateFrame().Data.Substring(2, 4);
                        dataFrame = new Packet(PacketHeader.VELOCITY_REQ, velocity);
                    }

                    if (null != dataFrame)
                    {
                        Robot.sendData(dataFrame);
                    }

                    if (!ControllerSettings.Instance.Controllers.Contains(controller))
                        throw new InvalidOperationException();

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
