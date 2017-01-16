using robotymobilne_projekt.Network;
using Server.Networking.Responses;

namespace Server.Networking.Requests
{
    public class PositionReq : IRequest
    {
        private byte[] data;

        public PositionReq(byte[] data)
        {
            this.data = data;
        }

        public IResponse execute(ServerMode serverMode)
        {
            throw new System.NotImplementedException();
        }
    }
}
