using System;
using robotymobilne_projekt.Devices;
using robotymobilne_projekt.Utils.AppLogger;
using robotymobilne_projekt.Network.Responses;

namespace robotymobilne_projekt.Network
{
    public class ServerMode : IConnectionMode
    {
        private RobotModel robot;
        private ServerService serverSocket;
        private readonly ResponseFactory responseFactory;

        #region Setters & Getters

        public RobotModel Robot
        {
            get { return robot; }
            set { robot = value; }
        }
        #endregion

        public ServerMode(ServerService socket)
        {
            serverSocket = socket;
            responseFactory = new ResponseFactory();

        }

        public void connect()
        {
            try
            {
                var data = Convert.ToString(robot.ID);
                var packet = new Packet(PacketHeader.CONNECT_REQ, data);
                serverSocket?.send(packet);
                robot.Status = RobotModel.StatusE.CONNECTING;
            }
            catch
            {
                Logger.Instance.log(LogLevel.ERROR, "Could not connect to server");
            }
        }

        public void disconnect()
        {
            try
            {
                var data = Convert.ToString(robot.ID);
                var packet = new Packet(PacketHeader.DISCONNECT_REQ, data);
                serverSocket?.send(packet);
                robot.Status = RobotModel.StatusE.DISCONNECTED;
            }
            catch
            {
                Logger.Instance.log(LogLevel.WARNING, "Server is not responding.");
            }
        }

        public void send(Packet packet)
        {
             serverSocket.send(packet);
        }

        public void receive()
        {

        }
    }
}
