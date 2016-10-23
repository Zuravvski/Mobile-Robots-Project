using robotymobilne_projekt.Manual;
using robotymobilne_projekt.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace robotymobilne_projekt
{
    class RobotModel : RemoteDevice
    {
        private Point position;
        private int speed;
        private MOVE_DIRECTION direction;

        // Setters and getters
        public Point POSITION
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }
        public int SPEED
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }
        public MOVE_DIRECTION DIRECTION
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }

        public RobotModel(string name, string ip, int port) : base(name, ip, port)
        {
            direction = MOVE_DIRECTION.IDLE;
        }

        public override bool connect()
        {
            try
            {
                Logger.getLogger().log(string.Format("Connecting with robot with {0}...", this));
                socket.Connect(ip, port);
                receiverThread.Start();
                senderThread.Start();        
            }
            catch(Exception)
            {
                Logger.getLogger().log(string.Format("Cannot connect to robot with {0}.", this));
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
                Logger.getLogger().log(string.Format("An error occurred while disconnecting robot with {0}.", this));
            }
        }

        public override void receiver()
        {
            // TODO: Implementation
        }

        public override string ToString()
        {
            return "ID: " + name;
        }

        public bool isConnected()
        {
            return socket.Connected;
        }

        public override void sender()
        {
            // TODO: Implementation
            lock (this)
            {
                Monitor.Wait(this);
                while (!sendBuffer.IsEmpty)
                {
                    try
                    {
                        byte[] frame;
                        sendBuffer.TryDequeue(out frame);
                        socket.Send(frame);
                    }
                    catch (Exception ex)
                    {
                        Logger.getLogger().log("Could not send data frame to robot with ID: " + this, ex);
                    }
                }
            }
        }

        public enum MOVE_DIRECTION { IDLE, FORWARD, BACKWARD };
    }
}
