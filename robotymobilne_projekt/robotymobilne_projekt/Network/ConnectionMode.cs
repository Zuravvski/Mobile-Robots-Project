using Server.Networking;

namespace robotymobilne_projekt.Network
{
    public interface ConnectionMode
    {
        void connect();
        void disconnect();
        void send(Packet packet);
        void receive();
    }
}
