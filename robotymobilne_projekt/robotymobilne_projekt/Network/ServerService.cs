using System;
using System.Net.Sockets;
using robotymobilne_projekt.Settings;
using robotymobilne_projekt.Utils.AppLogger;

namespace robotymobilne_projekt.Network
{
    public class ServerService
    {
        private TcpClient socket;

        public ServerService()
        {
            socket = new TcpClient(AddressFamily.InterNetwork);
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
                socket.BeginConnect(ApplicationSettings.Instance.Ip, 
                    ApplicationSettings.Instance.Port, connectCallback, null);
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
                socket.EndConnect(result);
            }
            catch
            {
                Logger.Instance.log(LogLevel.ERROR, "Could not connect to server");
            }
        }

        public void disconnect()
        {
            socket?.Close();
        }
    }
}
