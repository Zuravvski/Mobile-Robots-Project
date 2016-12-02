using System;
using System.Net.Sockets;
using robotymobilne_projekt.Utils;
using robotymobilne_projekt.Utils.AppLogger;

namespace robotymobilne_projekt.Devices
{
    public abstract class RemoteDevice : ObservableObject
    {
        #region Enums

        public enum StatusE
        {
            CONNECTED,
            DISCONNECTED,
            CONNECTING
        };

        #endregion

        protected readonly string deviceName;
        private readonly string ip;
        private readonly int port;
        protected TcpClient tcpClient;
        protected NetworkStream networkStream;
        private StatusE status;

        #region Setters & Getters

        public string DeviceName
        {
            get { return deviceName; }
        }

        public TcpClient TcpClient
        {
            get { return tcpClient; }
            set { tcpClient = value; }
        }

        public bool Connected
        {
            get
            {
                if (null != tcpClient)
                {
                    return tcpClient.Connected;
                }
                return false;
            }
        }

        public StatusE Status
        {
            get { return status; }
            set
            {
                status = value;
                NotifyPropertyChanged("Status");
            }
        }

        #endregion

        protected RemoteDevice(string deviceName, string ip, int port)
        {
            this.deviceName = deviceName;

            // Network settings
            this.ip = ip;
            this.port = port;
            Status = StatusE.DISCONNECTED;
        }

        #region Connection

        public virtual void connect()
        {
            if (null == tcpClient)
            {
                tcpClient = new TcpClient(AddressFamily.InterNetwork);
            }

            if (status == StatusE.CONNECTING || status == StatusE.CONNECTED) return;

            Status = StatusE.CONNECTING;
            Logger.Instance.log(LogLevel.INFO, string.Format("Connecting with device: {0}...", this));
            tcpClient.BeginConnect(ip, port, connectCallback, tcpClient);
        }

        public void connectCallback(IAsyncResult result)
        {
            try
            {
                tcpClient = (TcpClient) result.AsyncState;
                tcpClient.EndConnect(result);

                if (tcpClient.Connected)
                {
                    // run all 
                    networkStream = tcpClient.GetStream();
                    receiveData();
                    Status = StatusE.CONNECTED;
                    Logger.Instance.log(LogLevel.INFO, string.Format("Connected to {0}.", this));
                }
            }
            catch (Exception ex)
            {
                Status = StatusE.DISCONNECTED;
                Logger.Instance.log(LogLevel.WARNING, string.Format("Could not connect to {0}.", this), ex);
            }
        }

        #endregion

        #region Disconnection

        public virtual void disconnect()
        {
            try
            {
                networkStream?.Close();
                TcpClient?.Close();
                TcpClient = null;
                networkStream = null;
                
                Status = StatusE.DISCONNECTED;
                Logger.Instance.log(LogLevel.INFO, string.Format("{0} disconnected.", this));
            }
            catch (Exception ex)
            {
                Status = StatusE.DISCONNECTED;
                Logger.Instance
                    .log(LogLevel.ERROR, string.Format("An error occurred while disconnecting with device: {0}.", this), ex);
            }
        }

        #endregion

        #region Sending Data
        public abstract void sendData(string data);
        protected abstract void sendCallback(IAsyncResult result);
        #endregion

        #region Receiving Data
        protected abstract void receiveData();
        protected abstract void receiveCallback(IAsyncResult result);
        #endregion
    }
}
