using System.Security.Cryptography.X509Certificates;

namespace Server.Networking.Responses
{
    public interface IResponse
    {
        Packet execute();
    }
}
