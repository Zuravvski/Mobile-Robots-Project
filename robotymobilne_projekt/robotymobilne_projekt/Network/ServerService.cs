using System;
using System.Net.Sockets;
using robotymobilne_projekt.Settings;
using robotymobilne_projekt.Utils.AppLogger;

namespace robotymobilne_projekt.Network
{
    public class ServerService : IDisposable
    {
        public TcpClient Socket { get; private set; }

        public ServerService()
        {
            Socket = new TcpClient(AddressFamily.InterNetwork);
        }

        public void connect()
        {
            try
            {
                if (null == Socket)
                {
                    Socket = new TcpClient(AddressFamily.InterNetwork);
                }

                if (Socket.Connected) return;
                Socket.BeginConnect(ApplicationService.Instance.Ip, 
                    ApplicationService.Instance.Port, connectCallback, null);
            }
            catch
            {
                Logger.Instance.log(LogLevel.ERROR, "Could not connect to server");
            }

        }

        private void connectCallback(IAsyncResult result)
        {
            try
            {
                Socket.EndConnect(result);
            }
            catch
            {
                Logger.Instance.log(LogLevel.ERROR, "Could not connect to server");
            }
        }

        public void disconnect()
        {
            Socket?.Close();
        }

        public void Dispose()
        {
            Socket?.Close();
            ((IDisposable) Socket)?.Dispose();
        }
    }
}
