using MobileRobots.Utils;
using robotymobilne_projekt.Devices.Network;
using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace MobileRobots
{
    abstract class RemoteDevice
    {
        protected string deviceName;
        protected string ip;
        protected int port;
        protected TcpClient tcpClient;
        protected NetworkStream networkStream;

        // setters and getters
        public string NAME
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
        public int PORT
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
        public TcpClient TCPCLIENT
        {
            get
            {
                return tcpClient;
            }
        }

        public RemoteDevice(string deviceName, string ip, int port)
        {
            this.deviceName = deviceName;

            // Network settings
            this.ip = ip;
            this.port = port;
            tcpClient = new TcpClient(AddressFamily.InterNetwork);
        }

        public virtual bool connect()
        {
            Logger.getLogger().log(string.Format("Connecting with device: {0}...", this));
            tcpClient.BeginConnect(ip, port, new AsyncCallback(connectCallback), tcpClient);
            return tcpClient.Connected;
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

                Logger.getLogger().log(string.Format("Connected to {0}.", this));
            }
            catch(Exception ex)
            {
                Logger.getLogger().log(string.Format("Could not connect to {0}.", this), ex);
            }
        }

        public virtual void disconnect()
        {
            try
            {
                tcpClient.GetStream().Close();
                tcpClient.Close();
                tcpClient = new TcpClient(AddressFamily.InterNetwork);

                Logger.getLogger().log(string.Format("{0} disconnected.", this));
            }
            catch (Exception ex)
            {
                Logger.getLogger().log(string.Format("An error occurred while disconnecting with device: {0}.", this), ex);
            }
        }

        // Adapt to generic Data frame objects
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
                Logger.getLogger().log("Could not send data to device: " + deviceName, ex);
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
                Logger.getLogger().log("Could not send data to device: " + deviceName, ex);
            }
        }

        protected virtual void receiveData()
        {
            try
            {
                byte[] receiveBuffer = new byte[28];
                networkStream.BeginRead(receiveBuffer, 0, receiveBuffer.Length, new AsyncCallback(receiveCallback), tcpClient);
            }
            catch(Exception)
            {
                Logger.getLogger().log("Lost connection with remote device.");
            }
        }

        protected virtual void receiveCallback(IAsyncResult result)
        {
            try
            {
                int bytesRead = networkStream.EndRead(result);
                Thread.Sleep(5000);

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
                Logger.getLogger().log("Lost connection with remote device.");
                disconnect(); // current solution
            }
        }
    }
}
