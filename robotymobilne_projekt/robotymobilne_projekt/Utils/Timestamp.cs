using System;

namespace robotymobilne_projekt.Utils
{
    class Timestamp
    {
        public static string getDatestamp()
        {
            return string.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now);
        }

        public static string getTimestamp()
        {
            return string.Format("{0:HH:mm:ss}", DateTime.Now);
        }

        public static string getFileDateStamp()
        {
            return string.Format("{0:dd-MM-yyyy HH-mm-ss}", DateTime.Now);
        }
    }
}
