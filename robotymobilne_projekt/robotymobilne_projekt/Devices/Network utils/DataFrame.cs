using System.Text.RegularExpressions;

namespace robotymobilne_projekt.Devices.Network
{
    // Simple wrapper for managing data frames
    public abstract class DataFrame<T>
    {
        protected string data;

        public DataFrame(string data)
        {
            this.data = "[" + data + "]";
        }

        public DataFrame(byte[] data)
        {
            this.data = System.Text.Encoding.ASCII.GetString(data);
        }

        /// <summary>
        /// Returns frame data without frame wrapper.
        /// </summary>
        public string getRawData()
        {
            return data.Replace("[", string.Empty).Replace("]", string.Empty);
        }

        /// <summary>
        /// Returns ready to send byte-encoded data frame.
        /// </summary>
        public byte[] getFrame()
        {
            return System.Text.Encoding.ASCII.GetBytes(data);
        }

        /// <summary>
        /// Returns frame in string format.
        /// </summary>
        public string getString()
        {
            return data;
        }

        /// <summary>
        /// Updates given model with retreived data from frame.
        /// </summary>
        public abstract void parseFrame(T obj);
    }
}
