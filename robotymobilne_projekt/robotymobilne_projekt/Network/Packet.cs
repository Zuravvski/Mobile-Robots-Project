using robotymobilne_projekt.Settings;
using System;
using System.Text;

namespace robotymobilne_projekt.Network
{
    public class Packet
    {
        private readonly PacketHeader header;
        private readonly string data;

        #region Getters

        public PacketHeader Header
        {
            get { return header; }
        }

        public string Data
        {
            get { return data; }
        }

        #endregion

        public Packet(PacketHeader header, string data)
        {
            this.header = header;
            this.data = data;
        }

        public Packet(byte[] bytePacket)
        {
            var decoded = Encoding.ASCII.GetString(bytePacket);
            decoded = decoded.Replace("[", "").Replace("]", "");
            header = (PacketHeader)Enum.Parse(typeof(PacketHeader), decoded.Substring(0, 2), true);
            data = decoded.Substring(2);
        }

        public byte[] toBytes()
        {
            byte[] bytes;
            if (ApplicationService.Instance.AppMode == ApplicationService.ApplicationMode.SERVER)
            {
                var frame = '[' + ((int)header).ToString("00") + data + ']';
                bytes = Encoding.ASCII.GetBytes(frame);
            }
            else
            {
                var datawFrames = '[' + data + ']';
                bytes = Encoding.ASCII.GetBytes(datawFrames);
            }
            return bytes;             
        }
    }

    public enum PacketHeader
    {
        // Server requests & responses
        UNRECOGNISED = 0,
        CONNECT_REQ = 1,        
        CONNECT_ACK = 2,
        DISCONNECT_REQ = 9,

        // Client requests & responses
        POSITION_REQ = 3,
        POSITION_ACK = 4,
        VELOCITY_REQ = 5,
        VELOCITY_ACK = 6
    }

    public enum ResponseStatus
    {
        Failed,
        OK
    }
}
