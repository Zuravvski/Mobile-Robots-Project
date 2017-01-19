using System;
using Server.Networking;
using Server.Networking.Server.Networking;

namespace robotymobilne_projekt.Devices
{
    public class NullObjectRobot : RobotModel
    {
        public NullObjectRobot(string name, string ip, int port) : base(name, ip, port)
        {

        }

        public override void connect()
        {
            throw new NotSupportedException();
        }

        public override void disconnect()
        {
            throw new NotSupportedException();
        }

        public override void sendData(Packet data)
        {
            // null action
        }
    }
}
