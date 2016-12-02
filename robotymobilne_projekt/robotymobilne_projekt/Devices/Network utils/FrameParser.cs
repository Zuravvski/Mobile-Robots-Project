using System;

namespace robotymobilne_projekt.Devices.Network_utils
{
    public abstract class FrameParser<T>
    {
        protected T model;

        protected FrameParser(T model)
        {
            if (null == model)
            {
                throw new NullReferenceException();
            }
            this.model = model;
        }

        /// <summary>
        /// Updates given model with retreived data from frame.
        /// </summary>
        public abstract void parse(string data);

        public abstract void parse(byte[] data);
    }
}
