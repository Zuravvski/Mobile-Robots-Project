using MobileRobots;

namespace robotymobilne_projekt.Devices
{
    public class NullObjectRobot : RobotModel
    {
        public NullObjectRobot(string name, string ip, int port) : base(name, ip, port)
        {

        }

        public override void connect()
        {
            
        }

        public override void disconnect()
        {
            
        }

        public override void sendData(string data)
        {
            
        }

        //public override void run()
        //{
           
        //}

        //public override void handleController()
        //{
           
        //}
    }
}
