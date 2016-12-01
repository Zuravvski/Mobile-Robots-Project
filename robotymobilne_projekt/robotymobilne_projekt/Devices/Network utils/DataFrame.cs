namespace robotymobilne_projekt.Devices.Network_utils
{
    // Simple wrapper for managing data frames
    public abstract class DataFrame
    {
        protected readonly string data;

        public DataFrame(string data)
        {
            this.data = "[" + data + "]";
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
    }
}
