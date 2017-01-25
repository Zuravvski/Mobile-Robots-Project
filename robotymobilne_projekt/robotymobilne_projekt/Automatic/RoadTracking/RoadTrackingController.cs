using robotymobilne_projekt.Manual;
using robotymobilne_projekt.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace robotymobilne_projekt.Automatic.RoadTracking
{
    public class RoadTrackingController : AbstractController
    {
        public int robotPosX { set; get; }
        public int robotPosY { set; get; }
        public int robotAngle { set; get; }
        private List<Point> points;


        public RoadTrackingController(ref List<Point> points)
        {
            this.points = points;
        }

        public override Tuple<double, double> execute()
        {
            rotate(points);
            return new Tuple<double, double>(SpeedL, SpeedR);
        }

        //its good to have deadbonds just because of lack of accuracy between motorL and motorR (although this algorithm will self-correct it!)
        //another thing is that small misstracking wont be noticeable - why not abuse it >:-D
        double angleDeadbond = 10;   //type number in degrees
        double distanceDeadbond = 5;    // <-- THIS MUST BE > 0. Otherwise after "missing" target point it might just turn 180deg to reach it...


        /*given : 
         * list of points (here: List<Points> points) (to-go coordinates, sorted)
         * actual position of a robot (here: Point robotPos) along with its angle (here: robotAngle) according to X0-Y0 point (bottom-left corner)
         * obstacle coordinates (center point and radius?) <-- wat do? break path? find different way around it? must be constantly refreshed with data from camera <-- not implemented yet */

        /*wat do:
         * take first point from to-go coordinates
         * turn robot to face this point  <--------------------------------------------------
         * move in straight line till will be in accepted distance from this point          |
         * take next point, repeat ----------------------------------------------------------
         * 
         * if no more points - stop ?? <-- to be discussed (possibly move to X0-Y0?)
         */

        private double calculateTargetAngle(int robotPosX, int robotPosY, Point targetPos)
        {
            double angle = Math.Atan2((targetPos.Y - robotPosY), (targetPos.X - robotPosX)) * 180 / Math.PI;  //in degrees
            return angle;
        }


        private void rotate(List<Point> points)
        {
            //double targetAngle = calculateTargetAngle(robotPosX, robotPosY, points[0]); //always points[0] because as soon as target point is reached it will be removed from list
            double targetAngle = 20;
            if (Math.Abs(robotAngle - targetAngle) >= angleDeadbond) //if has to turn - TURN        <--- what about using while()? Will it block other simultaneous actions?
            {
                if (robotAngle - targetAngle > 0)
                {
                    //turn left
                    SpeedL = RobotSettings.Instance.MaxSpeed;
                    SpeedR = -RobotSettings.Instance.MaxSpeed;
                }
                else
                {
                    //turn right
                    SpeedR = RobotSettings.Instance.MaxSpeed;
                    SpeedL = -RobotSettings.Instance.MaxSpeed;
                }
            }
            else //no need for turn - DRIVE
            {
                if (Math.Sqrt(Math.Abs(robotPosX - points[0].X) + Math.Abs(robotPosY - points[0].Y)) >= distanceDeadbond)  // if far from point
                {
                    //drive straight
                    SpeedL = RobotSettings.Instance.MaxSpeed;
                    SpeedR = RobotSettings.Instance.MaxSpeed;
                }
                else    //if close enough to target point
                {
                    points.RemoveAt(0); //delete first item from list  <-- after that it will repeat everything with another target point
                }
            }
        }
    }
}
