using Server.Networking.Server.Networking;

namespace Server.Networking.Responses
{
    public interface IResponse
    {
        Packet execute();
    }
}
