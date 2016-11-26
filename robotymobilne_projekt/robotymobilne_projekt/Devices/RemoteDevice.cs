using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
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
            CONNECTING,
            RECONNECTING
        };

        #endregion

        protected readonly string deviceName;
        protected string ip;
        protected int port;
        protected TcpClient tcpClient;
        protected NetworkStream networkStream;
        protected StatusE status;

        #region Setters & Getters

        public string DeviceName
        {
            get { return deviceName; }
        }

        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        public TcpClient TcpClient
        {
            get { return tcpClient; }
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
            tcpClient = new TcpClient(AddressFamily.InterNetwork);
            Status = StatusE.DISCONNECTED;
        }

        #region Connection

        public virtual void connect()
        {
            if (tcpClient.Connected) return;

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
                }

                Status = StatusE.CONNECTED;
                Logger.Instance.log(LogLevel.INFO, string.Format("Connected to {0}.", this));
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
                if (!tcpClient.Connected) return;

                tcpClient.GetStream().Close();
                tcpClient.Close();
                tcpClient = new TcpClient(AddressFamily.InterNetwork);
                Status = StatusE.DISCONNECTED;

                Logger.Instance.log(LogLevel.INFO, string.Format("{0} disconnected.", this));
            }
            catch (Exception ex)
            {
                Status = StatusE.DISCONNECTED;
                Logger.Instance
                    .log(LogLevel.ERROR, string.Format("An error occurred while disconnecting with device: {0}.", this),
                        ex);
            }
        }

        #endregion

        #region Sending Data

        public virtual void sendData(string data)
        {
            try
            {
                var frameToSend = System.Text.Encoding.ASCII.GetBytes(data);
                networkStream.BeginWrite(frameToSend, 0, frameToSend.Length, sendCallback, tcpClient);
            }
            catch (IOException)
            {
                Status = StatusE.DISCONNECTED;
                Logger.Instance
                    .log(LogLevel.INFO, string.Format("Lost connection with {0}. Connection terminated by host.", this));
            }
            catch
            {
                Logger.Instance.log(LogLevel.INFO, "Could not send data to device: " + deviceName);
            }
        }

        protected void sendCallback(IAsyncResult result)
        {
            try
            {
                networkStream.EndWrite(result);
            }
            catch (Exception)
            {
                Status = StatusE.DISCONNECTED;
                disconnect();
            }
        }

        #endregion

        #region Receiving Data

        protected virtual void receiveData()
        {
            try
            {
                var receiveBuffer = new byte[28];
                networkStream.BeginRead(receiveBuffer, 0, receiveBuffer.Length, receiveCallback, tcpClient);
            }
            catch
            {
                disconnect();
                Logger.Instance.log(LogLevel.WARNING, "Lost connection with remote device.");
            }
        }

        protected void receiveCallback(IAsyncResult result)
        {
            try
            {
                var bytesRead = networkStream.EndRead(result);
                Thread.Sleep(1000);
                receiveData();
            }
            catch (Exception)
            {
                disconnect();
                Logger.Instance.log(LogLevel.WARNING, "Lost connection with remote device.");
            }
        }
        #endregion
    }
}
