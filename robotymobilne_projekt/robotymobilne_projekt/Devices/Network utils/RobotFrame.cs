using MobileRobots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotymobilne_projekt.Devices.Network
{
    class RobotFrame : DataFrame<RobotModel>
    {
        const int FRAME_SIZE = 28;

        public RobotFrame(string data) : base(data)
        {

        }

        public RobotFrame(byte[] data) : base(data)
        {

        }

        public override void parseFrame(RobotModel obj)
        {
            // Battery state is the only data that is actually used right now
            if(FRAME_SIZE == data.Length)
            {
                obj.Battery = getBattery();
                //obj.Status = getStatus();
            }
        }

        private int getBattery()
        {
            string bat1 = data.Substring(5, 2);
            string bat2 = data.Substring(3, 2);
            string bat3 = bat1 + bat2;
            int BatteryStatus = UInt16.Parse(bat3, System.Globalization.NumberStyles.HexNumber);
            return BatteryStatus;
        }

        private int getStatus()
        {
            string status = data.Substring(0, 2);
            return int.Parse(status, System.Globalization.NumberStyles.HexNumber);
        }
    }
}
