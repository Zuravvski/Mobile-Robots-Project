using System.ComponentModel;

namespace Server.Networking
{
    using System;
    using System.Text;

    namespace Server.Networking
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
                header = (PacketHeader)Enum.Parse(typeof(PacketHeader), decoded.Substring(1, 2), true);
                data = decoded.Substring(3, decoded.Length - 1);
            }

            public byte[] toBytes()
            {
                var frame = '[' + ((int) header).ToString("00") + data + ']';
                var bytes = Encoding.ASCII.GetBytes(frame);
                return bytes;             
            }
        }

        public enum PacketHeader
        {
            // Server requests & responses
            DISCONNECT_REQ = 0,
            CONNECT_REQ = 1,        
            CONNECT_ACK = 2,

            //// Mode switching 
            //MONITOR_MODE = 17, // position only
            //CONTROL_MODE = 33, // driving and chosen robots only
            //MONCON_MODE = 49,  // 2 above

            // Client requests & responses
            POSITION_REQ = 3,
            POSITION_ACK = 4,
            VELOCITY_REQ = 5,
            VELOCITY_ACK = 6
        }

        public enum ReponseStatus
        {
            Failed,
            OK
        }
    }
}
