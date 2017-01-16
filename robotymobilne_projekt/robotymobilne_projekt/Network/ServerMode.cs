using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using robotymobilne_projekt.Devices;
using robotymobilne_projekt.Utils.AppLogger;
using Server.Networking;

namespace robotymobilne_projekt.Network
{
    public class ServerMode : ConnectionMode
    {
        private RobotModel robot;
        private readonly NetworkStream networkStream;
        private TcpClient socket;

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
            networkStream = robot.Socket.GetStream();
        }

        public void connect()
        {
            try
            {
                var data = Convert.ToString(robot.ID);
                var packet = new Packet(PacketHeader.CONNECT_REQ, Encoding.ASCII.GetBytes(data));
                robot.sendData(packet);
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
                var packet = new Packet(PacketHeader.DISCONNECT_REQ, Encoding.ASCII.GetBytes(data));
                robot.sendData(packet);
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
                var buffer = packet.serialize();
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
                networkStream.BeginRead(buffer, 0, buffer.Length, receiveCallback, buffer);
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
