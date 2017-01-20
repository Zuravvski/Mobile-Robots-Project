using Server.Networking.Requests;

namespace robotymobilne_projekt.Network
{
    public class RequestFactory
    {
        public IRequest getRequest(Packet packet)
        {
            IRequest request = null;

            if (null == packet) return request;

            switch (packet.Header)
            {
                case PacketHeader.CONNECT_ACK:
                    request = new ConnectReq(packet.Data);
                    break;

                case PacketHeader.DISCONNECT_REQ:
                    request = new DisconnectReq(packet.Data);
                    break;

                case PacketHeader.VELOCITY_REQ:
                    request = new VelocityReq(packet.Data);
                    break;

                case PacketHeader.POSITION_REQ:
                    request = new PositionReq(packet.Data);
                    break;
            }

            return request;
        }
    }
}
