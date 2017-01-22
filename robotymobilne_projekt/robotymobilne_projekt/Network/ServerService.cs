using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using robotymobilne_projekt.Network.Responses;
using robotymobilne_projekt.Settings;
using robotymobilne_projekt.Utils.AppLogger;

namespace robotymobilne_projekt.Network
{
    public class ServerService : IDisposable
    {
        private TcpClient socket;
        private NetworkStream networkStream;
        private readonly ResponseFactory responseFactory;

        public bool Connected => null != socket && socket.Connected;

        public ServerService()
        {
            socket = new TcpClient(AddressFamily.InterNetwork);
            responseFactory = new ResponseFactory();
        }

        public void connect()
        {
            try
            {
                if (null == socket)
                {
                    socket = new TcpClient(AddressFamily.InterNetwork);
                }

                if (socket.Connected) return;
                
                socket.Connect(ApplicationService.Instance.Ip, ApplicationService.Instance.Port);
                if (socket.Connected)
                {
                    Logger.Instance.log(LogLevel.INFO, "Connected to server.");
                    networkStream = socket.GetStream();
                    receive();
                }
            }
            catch
            {
                Logger.Instance.log(LogLevel.ERROR, "Could not connect to server");
            }

        }

        public void disconnect()
        {
            networkStream?.Close();
            socket?.Close();
            networkStream = null;
            socket = null;
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
            catch (IOException)
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

        private void receive()
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
                var buffer = (byte[])result.AsyncState;
                var bytesRead = networkStream.EndRead(result);
                Array.Resize(ref buffer, bytesRead);
                var response = responseFactory.getResponse(new Packet(buffer));
                Task.Run(() => response?.execute());
                Thread.Sleep(ControllerSettings.Instance.Latency);
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

        public void Dispose()
        {
            disconnect();
        }
    }
}
