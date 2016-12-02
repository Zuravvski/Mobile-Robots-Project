namespace robotymobilne_projekt.Devices.Network_utils
{
    public class RobotFrame : DataFrame
    {
        private const int FRAME_SIZE = 8; // 6 signs of data + [] 

        public RobotFrame(string data) : base(data)
        {

        }
    }
}
