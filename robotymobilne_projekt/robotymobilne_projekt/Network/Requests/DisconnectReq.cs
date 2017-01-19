using System;
using robotymobilne_projekt.Network;
using Server.Networking.Responses;

namespace Server.Networking.Requests
{
    public class DisconnectReq : IRequest
    {
        private readonly string data;

        public DisconnectReq(string data)
        {
            this.data = data;
        }

        public IResponse execute(ServerMode serverMode)
        {
            throw new NotImplementedException();
        }
    }
}
