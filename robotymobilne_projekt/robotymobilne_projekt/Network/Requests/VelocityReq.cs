using System;
using robotymobilne_projekt.Network;
using Server.Networking.Responses;


namespace Server.Networking.Requests
{
    public class VelocityReq : IRequest
    {
        private byte[] data;

        public VelocityReq(byte[] data)
        {
            this.data = data;
        }


        public IResponse execute(ServerMode serverMode)
        {
            throw new NotImplementedException();
        }
    }
}
