using System;
using robotymobilne_projekt.Network;
using Server.Networking.Responses;


namespace Server.Networking.Requests
{
    public class VelocityReq : IRequest
    {
        private string data;

        public VelocityReq(string data)
        {
            this.data = data;
        }


        public IResponse execute(ServerMode serverMode)
        {
            throw new NotImplementedException();
        }
    }
}
