using System;
using robotymobilne_projekt.Network;
using Server.Networking.Responses;


namespace Server.Networking.Requests
{
    public class ConnectReq : IRequest
    {
        private readonly byte[] data;

        public ConnectReq(byte[] data)
        {
            this.data = data;
        }

        public IResponse execute(ServerMode serverMode)
        {
            throw new NotImplementedException();
        }
    }
}
