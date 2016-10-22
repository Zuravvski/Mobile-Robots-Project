using robotymobilne_projekt.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace robotymobilne_projekt
{
    abstract class RemoteDevice
    {
        protected string name;
        protected string ip;
        protected int port;
        protected Socket socket;
        protected Thread senderThread;
        protected Thread receiverThread;

        // setters and getters
        public string NAME
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
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
        public Socket SOCKET
        {
            get
            {
                return socket;
            }
        }

        // Managing data received from socket
        protected ConcurrentQueue<byte[]> sendBuffer;
        protected ConcurrentQueue<byte[]> receiveBuffer;

        public RemoteDevice(string deviceName, string ip, int port)
        {
            this.name = deviceName;
            this.ip = ip;
            this.port = port;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sendBuffer = new ConcurrentQueue<byte[]>();
            receiveBuffer = new ConcurrentQueue<byte[]>();
            senderThread = new Thread(sender);
            receiverThread = new Thread(receiver);
        }

        // Abstract methods
        public abstract bool connect();
        public abstract void disconnect();
        public abstract void receiver();
        public abstract void sender();

        // Implemented methods
        public void sendDataFrame(string data)
        {
            try
            {
                byte[] sendbyte = System.Text.Encoding.ASCII.GetBytes("[" + data + "]");
                sendBuffer.Enqueue(sendbyte);
                Monitor.Pulse(this);
            }
            catch(Exception ex)
            {
                Logger.getLogger().log("Couldn't send data to device: " + name, ex);
            }
        }
    }
}
