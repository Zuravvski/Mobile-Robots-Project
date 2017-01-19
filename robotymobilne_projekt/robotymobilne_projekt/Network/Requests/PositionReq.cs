using robotymobilne_projekt.Network;
using Server.Networking.Responses;

namespace Server.Networking.Requests
{
    public class PositionReq : IRequest
    {
        private string data;

        public PositionReq(string data)
        {
            this.data = data;
        }

        public IResponse execute(ServerMode serverMode)
        {
            throw new System.NotImplementedException();
        }
    }
}
