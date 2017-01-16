using robotymobilne_projekt.Network;
using Server.Networking.Responses;

namespace Server.Networking.Requests
{
    public interface IRequest
    {
        IResponse execute(ServerMode serverMode);
    }
}
