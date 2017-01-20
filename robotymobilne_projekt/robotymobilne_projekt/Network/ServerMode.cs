using System;
using System.IO;
using System.Net.Sockets;
using robotymobilne_projekt.Devices;
using robotymobilne_projekt.Utils.AppLogger;
using robotymobilne_projekt.Network.Responses;
using Server.Networking.Responses;

namespace robotymobilne_projekt.Network
{
    public class ServerMode : ConnectionMode
    {
        private RobotModel robot;
        private NetworkStream networkStream;
        private readonly TcpClient socket;
        private readonly ResponseFactory responseFactory;

        #region Setters & Getters

        public RobotModel Robot
        {
            get { return robot; }
            set { robot = value; }
        }
        #endregion

        public ServerMode(TcpClient socket)
        {
            this.socket = socket;
            responseFactory = new ResponseFactory();

            if (socket.Connected)
            {
                networkStream = socket?.GetStream();
                receive();
            }
        }

        public void connect()
        {
            try
            {
                var data = Convert.ToString(robot.ID);
                var packet = new Packet(PacketHeader.CONNECT_REQ, data);
                send(packet);
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
                robot.sendData(packet);
                robot.Status = RobotModel.StatusE.DISCONNECTED;
            }
            catch
            {
                Logger.Instance.log(LogLevel.WARNING, "Server is not responding.");
            }

        }

        public void send(Packet packet)
        {
            try
            {
                var buffer = packet.toBytes();

                if (networkStream == null && socket.Connected)
                {
                    networkStream = socket?.GetStream();
                }
                networkStream?.BeginWrite(buffer, 0, buffer.Length, sendCallback, null);
            }
            catch(IOException)
            {
                disconnect();
            }
            catch 
            {
                Logger.Instance.log(LogLevel.ERROR, "Could not send data to server");
            }
        }

        private void sendCallback(IAsyncResult result)
        {
            try
            {
                networkStream.EndWrite(result);
            }
            catch (IOException)
            {
                disconnect();
            }
            catch 
            {
                Logger.Instance.log(LogLevel.ERROR, "Could not send data to server");
            }
        }

        public void receive()
        {
            try
            {
                var buffer = new byte[128];
                if (networkStream != null && socket.Connected)
                {
                    networkStream = socket?.GetStream();
                }
                networkStream?.BeginRead(buffer, 0, buffer.Length, receiveCallback, buffer);
            }
            catch (IOException)
            {
                disconnect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void receiveCallback(IAsyncResult result)
        {
            try
            {
                var buffer = (byte[]) result.AsyncState;
                var bytesRead = networkStream.EndRead(result);
                Array.Resize(ref buffer, bytesRead);
                IResponse response = responseFactory.getResponse(new Packet(buffer));
                response?.execute(robot);
                receive();
            }
            catch (IOException)
            {
                disconnect();
            }
            catch 
            {
                Logger.Instance.log(LogLevel.INFO, "Server packet has been lost");
            }
        }
    }
}
