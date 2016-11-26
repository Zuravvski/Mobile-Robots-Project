using System;

namespace robotymobilne_projekt.Devices.Network_utils
{
    public class RobotFrame : DataFrame<RobotModel>
    {
        private const int FRAME_SIZE = 28; // 28 signs of data + [] 

        public RobotFrame(string data) : base(data)
        {

        }

        public RobotFrame(byte[] data) : base(data)
        {

        }

        public override void parseFrame(RobotModel obj)
        {
            if (FRAME_SIZE != data.Length) return;

            obj.Battery = getBattery(obj.Battery);
            obj.Status = getStatus(obj.Status);
        }

        private int getBattery(int oldValue)
        {
            try
            {
                var bat1 = data.Substring(5, 2);
                var bat2 = data.Substring(3, 2);
                var bat3 = bat1 + bat2;
                return UInt16.Parse(bat3, System.Globalization.NumberStyles.HexNumber);
            }
            catch (Exception)
            {
                return oldValue;
            }
        }

        private RemoteDevice.StatusE getStatus(RemoteDevice.StatusE oldStatus)
        {
            try
            {
                var statusFrame = data.Substring(1, 2);
                var statusInt = int.Parse(statusFrame, System.Globalization.NumberStyles.HexNumber);
                return statusInt == 5 ? RemoteDevice.StatusE.DISCONNECTED : RemoteDevice.StatusE.CONNECTED;
            }
            catch (Exception)
            {
                return oldStatus;
            } 
        }
    }
}
