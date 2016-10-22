using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace robotymobilne_projekt
{
    class RobotModel : RemoteDevice
    {
        private string name;
        private Point position;
        private int speed;
        private string ip;
        private int port;

        public RobotModel(string name, string ip, int port)
        {
            this.name = name;
            this.ip = ip;
            this.port = port;

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            speed = 0;
        }

        public override bool connect()
        {
            try
            {
                socket.Connect(ip, port);
            }
            catch(Exception)
            {

            }

            return socket.Connected;
        }

        public override void disconnect()
        {
            try
            {
                socket.Disconnect(true);
            }
            catch(Exception)
            {

            }
        }

        public override string receive()
        {
            return "lol";
        }

        public override string ToString()
        {
            return "ID: " + name;
        }

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

        public bool isConnected()
        {
            return socket.Connected;
        }
    }
}
