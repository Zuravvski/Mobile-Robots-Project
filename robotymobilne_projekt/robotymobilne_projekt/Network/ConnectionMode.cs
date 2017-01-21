namespace robotymobilne_projekt.Network
{
    public interface IConnectionMode
    {
        void connect();
        void disconnect();
        void send(Packet packet);
        void receive();
    }
}
