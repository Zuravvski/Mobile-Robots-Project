using robotymobilne_projekt.Devices;

namespace Server.Networking.Responses
{
    public interface IResponse
    {
        void execute(RobotModel robot);
    }
}
