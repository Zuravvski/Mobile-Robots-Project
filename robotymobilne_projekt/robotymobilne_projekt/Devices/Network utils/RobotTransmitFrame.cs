using MobileRobots;
using robotymobilne_projekt.Devices.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotymobilne_projekt.Devices.Network_utils
{
    public class RobotTransmitFrame : DataFrame<RobotModel>
    {

        public RobotTransmitFrame(string data) : base(data)
        {

        }

        public RobotTransmitFrame(byte[] data) : base(data)
        {

        }


        public override void parseFrame(RobotModel obj)
        {
            try
            {
                obj.SpeedL = getSpeedL();
                obj.SpeedR = getSpeedR();
            }
            catch (Exception) { }
        }

        private double getSpeedL()
        {
            return double.Parse(data.Substring(3, 2));
        }

        private double getSpeedR()
        {
            return double.Parse(data.Substring(5, 2));
        }
    }
}
