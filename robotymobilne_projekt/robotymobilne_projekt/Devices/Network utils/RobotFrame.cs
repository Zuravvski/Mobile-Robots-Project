using System;

namespace robotymobilne_projekt.Devices.Network_utils
{
    public class RobotFrame : DataFrame<RobotModel>
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
            if (FRAME_SIZE != data.Length)
            {
                return;
            }
            obj.Battery = getBattery();
            obj.Status = getStatus();
        }

        private int getBattery()
        {
            var bat1 = data.Substring(5, 2);
            var bat2 = data.Substring(3, 2);
            var bat3 = bat1 + bat2;
            int batteryStatus = UInt16.Parse(bat3, System.Globalization.NumberStyles.HexNumber);
            return batteryStatus;
        }

        private RemoteDevice.StatusE getStatus()
        {
            var statusFrame = data.Substring(0, 2);
            var statusInt = int.Parse(statusFrame, System.Globalization.NumberStyles.HexNumber);
            if(statusInt == 5)
            {
                return RemoteDevice.StatusE.DISCONNECTED;
            }
            else
            {
                return RemoteDevice.StatusE.CONNECTED;
            }
        }
    }
}
