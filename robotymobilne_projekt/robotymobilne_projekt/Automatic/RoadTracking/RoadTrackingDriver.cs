using System.Threading;
using robotymobilne_projekt.Devices;
using robotymobilne_projekt.Settings;
using System.Collections.Generic;
using System.Windows;
using robotymobilne_projekt.Network;

namespace robotymobilne_projekt.Automatic.RoadTracking
{
    public class RoadTrackingDriver : RobotDriver
    {
        private RoadTrackingController controller;

        public RoadTrackingDriver(RobotModel robot, RoadTrackingController controller, ref List<Point> points) : base(robot, controller)
        {
            this.controller = controller;
        }

        protected override void run()
        {
            while (null != robot && null != controller && robot.Status != RobotModel.StatusE.DISCONNECTED)
            {
                try
                {
                    controller.robotPosX = robot.X;
                    controller.robotPosY = robot.Y;
                    controller.robotAngle = robot.Angle;

                    var reading = controller.execute();
                    robot.SpeedL = reading.Item1;
                    robot.SpeedR = reading.Item2;
                    Packet dataFrame;
                    var velocity = robot.ID.ToString("00") + robot.calculateFrame().Data.Substring(2, 4);
                    dataFrame = new Packet(PacketHeader.VELOCITY_REQ, velocity);

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
