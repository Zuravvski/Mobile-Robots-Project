using Server.Networking.Responses;

namespace robotymobilne_projekt.Network.Responses
{
    public class ResponseFactory
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

                case PacketHeader.POSITION_ACK:
                    response = new PositionResponse(packet.Data);
                    break;
            }

            return response;
        }
    }
}
