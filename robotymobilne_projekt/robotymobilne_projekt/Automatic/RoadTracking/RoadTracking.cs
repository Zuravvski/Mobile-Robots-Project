using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace robotymobilne_projekt.Automatic.RoadTracking
{
    class RoadTracking
    {
        //its good to have deadbonds just because of lack of accuracy between motorL and motorR (although this algorithm will self-correct it!)
        //another thing is that small misstracking wont be noticeable - why not abuse it >:-D
        double angleDeadbond = 0 * Math.PI / 180;   //type number in degrees
        double distanceDeadbond = 0;    // <-- THIS MUST BE > 0. Otherwise after "missing" target point it might just turn 180deg to reach it...


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

        public RoadTracking()
        {

        }

        private double calculateTargetAngle(Point robotPos, Point targetPos)
        {
            double angle = Math.Acos((targetPos.Y - robotPos.Y) / (targetPos.X - robotPos.X));  //in radians
            return angle;
        }


        private void rotate(int robotAngle, Point robotPos, List<Point> points)
        {
            double targetAngle = calculateTargetAngle(robotPos, points[0]); //always points[0] because as soon as target point is reached it will be removed from list

            if (Math.Abs(robotAngle - targetAngle) >= angleDeadbond) //if has to turn - TURN        <--- what about using while()? Will it block other simultaneous actions?
            {
                if (robotAngle - targetAngle > 0)
                {
                    //turn left
                }
                else
                {
                    //turn right
                }
            }
            else //no need for turn - DRIVE
            {
                if (Math.Sqrt(Math.Abs(robotPos.X - points[0].X) + Math.Abs(robotPos.Y - points[0].Y)) >= distanceDeadbond)  // if far from point
                {
                    //drive straight
                }
                else    //if close enough to target point
                {
                    points.RemoveAt(0); //delete first item from list  <-- after that it will repeat everything with another target point
                }
            }
        }
    }
}
