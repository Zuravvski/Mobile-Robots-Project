using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using robotymobilne_projekt.Devices;
using robotymobilne_projekt.Devices.Network_utils;
using robotymobilne_projekt.Settings;
using robotymobilne_projekt.Utils.AppLogger;

namespace robotymobilne_projekt.Network
{
    public class DirectMode : ConnectionMode
    {
        private RobotModel robot;
        private NetworkStream networkStream;
        private readonly RobotFrameParser parser;

        #region Setters & Getters

        public RobotModel Robot
        {
            get
            {
                return robot;
            }
            set { robot = value; }
        }

        #endregion

        public DirectMode(RobotModel robot)
        {
            this.robot = robot;
            parser = new RobotFrameParser(robot);
        }

        public void connect()
        {
            if (null == Robot.Socket)
            {
                Robot.Socket = new TcpClient(AddressFamily.InterNetwork);
            }

            if (Robot.Status == RobotModel.StatusE.CONNECTING || Robot.Status == RobotModel.StatusE.CONNECTED) return;

            Robot.Status = RobotModel.StatusE.CONNECTING;
            Logger.Instance.log(LogLevel.INFO, string.Format("Connecting with device: {0}...", this));
            Robot.Socket.BeginConnect(Robot.IP, Robot.Port, connectCallback, Robot.Socket);
        }

        private void connectCallback(IAsyncResult result)
        {
            try
            {
                Robot.Socket = (TcpClient)result.AsyncState;
                Robot.Socket.EndConnect(result);

                if (!Robot.Socket.Connected) return;

                networkStream = Robot.Socket.GetStream();
                receive();
                Robot.Status = RobotModel.StatusE.CONNECTED;
                Logger.Instance.log(LogLevel.INFO, string.Format("Connected to {0}.", this));
            }
            catch (Exception ex)
            {
                Robot.Status = RobotModel.StatusE.DISCONNECTED;
                Logger.Instance.log(LogLevel.WARNING, string.Format("Could not connect to {0}.", this), ex);
            }
        }

        public void disconnect()
        {
            try
            {
                networkStream?.Close();
                Robot.Socket?.Close();
                Robot.Socket = null;
                networkStream = null;

                Robot.Status = RobotModel.StatusE.DISCONNECTED;
                Logger.Instance.log(LogLevel.INFO, string.Format("{0} disconnected.", this));
            }
            catch (Exception ex)
            {
                Robot.Status = RobotModel.StatusE.DISCONNECTED;
                Logger.Instance
                    .log(LogLevel.ERROR, string.Format("An error occurred while disconnecting with device: {0}.", this), ex);
            }
        }

        public void send(Packet packet)
        {
            try
            {
                if (Robot.Status != RobotModel.StatusE.CONNECTED) return;

                var frameToSend = packet.toBytes();
                networkStream.BeginWrite(frameToSend, 0, frameToSend.Length, sendCallback, robot.Socket);
            }
            catch (IOException)
            {
                disconnect();
                Logger.Instance.log(LogLevel.INFO,
                    string.Format("Device {0} disconnected. Connection terminated by host.", this));
            }
            catch
            {
                Logger.Instance.log(LogLevel.INFO, "Could not send data to device: " + robot.Name);
            }
        }

        private void sendCallback(IAsyncResult result)
        {
            try
            {
                networkStream.EndWrite(result);
            }
            catch
            {
                Logger.Instance.log(LogLevel.INFO, "Could not send data to device: " + robot.Name);
            }
        }

        public void receive()
        {
            try
            {
                var receiveBuffer = new byte[1024];
                networkStream.BeginRead(receiveBuffer, 0, receiveBuffer.Length, receiveCallback, receiveBuffer);
            }
            catch (IOException)
            {
                disconnect();
                Logger.Instance.log(LogLevel.INFO,
                    string.Format("Device {0} disconnected. Connection terminated by host.", this));
            }
            catch
            {
                Logger.Instance.log(LogLevel.INFO, string.Format("Could not retreive data from {0}", this));
            }
        }

        private void receiveCallback(IAsyncResult result)
        {
            try
            {
                var receiveBuffer = (byte[])result.AsyncState;
                var bytesRead = networkStream.EndRead(result);
                Array.Resize(ref receiveBuffer, bytesRead);

                if (RobotFrameParser.FRAME_SIZE == receiveBuffer.Length)
                {
                    parser.parse(receiveBuffer);
                }

                Thread.Sleep(ControllerSettings.Instance.Latency); // to provide equal send-receive ratio
                receive();
            }
            catch (Exception)
            {
                Logger.Instance.log(LogLevel.INFO, string.Format("Could not retreive data from {0}", this));
            }
        }
    }
}
