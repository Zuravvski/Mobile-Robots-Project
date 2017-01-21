using System;
using robotymobilne_projekt.Network;

namespace robotymobilne_projekt.Devices
{
    public class NullObjectRobot : RobotModel
    {
        public NullObjectRobot(int id, string ip, int port) : base(id, ip, port)
        {

        }

        public override void connect()
        {
        }

        public override void disconnect()
        {
        }

        public override void sendData(Packet data)
        {
        }

        public override string ToString()
        {
            return "NONE";
        }
    }
}
