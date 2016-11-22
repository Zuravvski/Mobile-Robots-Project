using MobileRobots.Utils.AppLogger;
using robotymobilne_projekt.Devices.Network;
using robotymobilne_projekt.Settings;
using robotymobilne_projekt.Utils;
using robotymobilne_projekt.Utils.AppLogger;
using System;
using System.Net.Sockets;
using System.Threading;

namespace MobileRobots
{
    public abstract class RemoteDevice : ObservableObject
    {
        #region Enums
        public enum StatusE { CONNECTED, DISCONNECTED, CONNECTING };
        #endregion

        protected string deviceName;
        protected string ip;
        protected int port;
        protected TcpClient tcpClient;
        protected NetworkStream networkStream;
        protected StatusE status;

        #region Setters & Getters
        public string DeviceName
        {
            get
            {
                return deviceName;
            }
            set
            {
                deviceName = value;
            }
        }
        public string IP
        {
            get
            {
                return ip;
            }
            set
            {
                ip = value;
            }
        }
        public int Port
        {
            get
            {
                return port;
            }
            set
            {
                port = value;
            }
        }
        public TcpClient TcpClient
        {
            get
            {
                return tcpClient;
            }
        }
        public bool Connected
        {
            get
            {
                if(null != tcpClient)
                {
                    return tcpClient.Connected;
                }
                return false;
            }
        }
        public StatusE Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                NotifyPropertyChanged("Status");
            }
        }
        #endregion

        public RemoteDevice(string deviceName, string ip, int port)
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
            if (!tcpClient.Connected)
            {
                Status = StatusE.CONNECTING;
                Logger.getLogger().log(LogLevel.INFO, string.Format("Connecting with device: {0}...", this));
                tcpClient.BeginConnect(ip, port, new AsyncCallback(connectCallback), tcpClient);
            }
        }

        public virtual void connectCallback(IAsyncResult result)
        {
            try
            {
                TcpClient client = (TcpClient)result.AsyncState;
                tcpClient.EndConnect(result);
               
                if(tcpClient.Connected)
                {
                    // run all 
                    networkStream = tcpClient.GetStream();
                    receiveData();
                }

                Status = StatusE.CONNECTED;
                Logger.getLogger().log(LogLevel.INFO, string.Format("Connected to {0}.", this));
            }
            catch(Exception ex)
            {
                Status = StatusE.DISCONNECTED;
                Logger.getLogger().log(LogLevel.WARNING, string.Format("Could not connect to {0}.", this), ex);
            }
        }
        #endregion

        #region Disconnection
        public virtual void disconnect()
        {
            try
            {
                if (tcpClient.Connected)
                {
                    tcpClient.GetStream().Close();
                    tcpClient.Close();
                    tcpClient = new TcpClient(AddressFamily.InterNetwork);
                    Status = StatusE.DISCONNECTED;
                  
                    Logger.getLogger().log(LogLevel.INFO, string.Format("{0} disconnected.", this));
                }
            }
            catch (Exception ex)
            {
                Status = StatusE.DISCONNECTED;
                Logger.getLogger().log(LogLevel.WARNING, string.Format("An error occurred while disconnecting with device: {0}.", this), ex);
            }
        }
        #endregion

        // Adapt to generic Data frame objects
        // Add reconnection mechanism
        #region Sending Data
        public virtual void sendData(string data)
        {
            try
            {
                if (null != networkStream && tcpClient.Connected)
                {
                    RobotFrame oFrame = new RobotFrame(data);
                    string stingFrame = oFrame.getString();
                    byte[] frameToSend = oFrame.getFrame();
                    networkStream.BeginWrite(frameToSend, 0, frameToSend.Length, new AsyncCallback(sendCallback), tcpClient);
                }
                else
                {
                    disconnect();
                }
            }
            catch(Exception ex)
            {
                Logger.getLogger().log(LogLevel.WARNING, "Could not send data to device: " + deviceName, ex);
                disconnect();
            }
        }

        protected virtual void sendCallback(IAsyncResult result)
        {
            try
            {
                TcpClient socket = (TcpClient)result.AsyncState;
                networkStream.EndWrite(result);
            }
            catch(Exception ex)
            {
                Logger.getLogger().log(LogLevel.WARNING, "Could not send data to device: " + deviceName, ex);
                disconnect();
            }
        }
        #endregion

        // Includes TODO!
        #region Receiving Data
        protected virtual void receiveData()
        {
            try
            {
                byte[] receiveBuffer = new byte[28];
                networkStream.BeginRead(receiveBuffer, 0, receiveBuffer.Length, new AsyncCallback(receiveCallback), tcpClient);
            }
            catch(Exception)
            {
                Logger.getLogger().log(LogLevel.WARNING, "Lost connection with remote device.");
            }
        }

        protected virtual void receiveCallback(IAsyncResult result)
        {
            try
            {
                int bytesRead = networkStream.EndRead(result);
                Thread.Sleep(1000);

                // Start another loop
                if (bytesRead != 0)
                {
                    // TODO: Update model (most probably wrap it in data frame to receive fields)
                    receiveData();
                }
                else
                {
                    // Handle disconnection
                    disconnect();
                    return;
                }
            }
            catch (Exception)
            {
                // Implement countdown signal event to handle 3 reconnections then close
                Logger.getLogger().log(LogLevel.WARNING, "Lost connection with remote device.");
                disconnect(); // current solution
            }
        }
        #endregion
    }
}
