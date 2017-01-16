using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Server.Networking
{
    [Serializable]
    public class Packet
    {
        private readonly PacketHeader header;
        private readonly byte[] data;
        private readonly bool serialized;

        #region Getters

        public PacketHeader Header
        {
            get { return header; }
        }

        public byte[] Data
        {
            get { return data; }
        }

        #endregion

        public Packet(PacketHeader header, byte[] data, bool serialized = false)
        {
            this.header = header;
            this.data = data;
            this.serialized = serialized;
        }

        public Packet(byte[] bytePacket)
        {
            if (serialized)
            {
                var binaryFormatter = new BinaryFormatter();
                var memoryStream = new MemoryStream(bytePacket);

                var packet = (Packet) binaryFormatter.Deserialize(memoryStream);
                header = packet.header;
                data = packet.data;
                memoryStream.Close();
            }
            else
            {
                data = bytePacket;
            }
        }

        public byte[] serialize()
        {
            var binaryFormatter = new BinaryFormatter();
            var memoryStream = new MemoryStream();

            binaryFormatter.Serialize(memoryStream, this);
            var buffer = memoryStream.ToArray();
            memoryStream.Close();
            return buffer;
        }
}

    public enum PacketHeader
    {
        // Server requests & responses
        DISCONNECT_REQ = 0, 
        CONNECT_REQ = 1,
        CONNECT_ACK = 2,

        // Mode switching 
        MONITOR_MODE = 17, // position only
        CONTROL_MODE = 33, // driving and chosen robots only
        MONCON_MODE = 49,  // 2 above

        // ServerMode requests & responses
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
