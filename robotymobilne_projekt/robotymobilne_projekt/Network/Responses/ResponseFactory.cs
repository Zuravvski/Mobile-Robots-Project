using Server.Networking.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotymobilne_projekt.Network.Responses
{
    class ResponseFactory
    {
        public IResponse getResponse(Packet packet)
        {
            IResponse response = null;

            if (null == packet) return response;

            switch (packet.Header)
            {
                case PacketHeader.CONNECT_ACK:
                    response = new ConnectAck(packet.Data);
                    break;
            }

            return response;
        }
    }
}
