using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace robotymobilne_projekt.Devices.Network_utils
{
    public class RobotFrameParser : FrameParser<RobotModel>
    {
        public static readonly int FRAME_SIZE = 28; // 28 signs of data + [] 

        public RobotFrameParser(RobotModel model) : base(model)
        {

        }

        public override void parse(byte[] data)
        {
            if (null == data) return;

            var frame = System.Text.Encoding.ASCII.GetString(data);
            parse(frame);
        }

        public override void parse(string data)
        {
            if (null == data || FRAME_SIZE != data.Length || !data.StartsWith("[")) return;

            model.Battery = getBattery(data);
            model.Status = getStatus(data);
            model.Sensors = getSensors(data);
        }

        private int getBattery(string data)
        {
            var bat1 = data.Substring(5, 2);
            var bat2 = data.Substring(3, 2);
            var bat3 = bat1 + bat2;
            return ushort.Parse(bat3, System.Globalization.NumberStyles.HexNumber);
        }

        private RemoteDevice.StatusE getStatus(string data)
        {
            var statusFrame = data.Substring(1, 2);
            var statusInt = int.Parse(statusFrame, System.Globalization.NumberStyles.HexNumber);
            return statusInt == 5 ? RemoteDevice.StatusE.DISCONNECTED : RemoteDevice.StatusE.CONNECTED;
        }

        private ObservableCollection<int> getSensors(string data)
        {
            ObservableCollection<int> newSensorsValues = new ObservableCollection<int>() { 0, 0, 0, 0, 0 };
            for (int i = 0, step = 7; i < 5; i++, step += 4)
            {
                var temp1 = data.Substring(step + 2, 2);
                var temp2 = data.Substring(step, 2);
                var sensor = temp1 + temp2;

                newSensorsValues[i] = int.Parse(sensor, System.Globalization.NumberStyles.HexNumber);
                //Debug.WriteLine(string.Format("Sensor {0} value is {1}", i+1, newSensorsValues[i]));
            }
            return newSensorsValues;
        }
    }
}
