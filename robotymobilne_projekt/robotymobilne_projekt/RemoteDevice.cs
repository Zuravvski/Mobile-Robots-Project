using System;
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
        protected Socket socket;
        protected Thread senderThread;
        protected Thread receiverThread;
        protected Thread receivedThread;

        public abstract bool connect();
        public abstract void disconnect();
        public abstract string receive();

        public void send(string data)
        {
            try
            {
                byte[] sendbyte = System.Text.Encoding.ASCII.GetBytes("[" + data + "]");
                socket.Send(sendbyte);
            }
            catch(Exception)
            {

            }
        }
    }
}
