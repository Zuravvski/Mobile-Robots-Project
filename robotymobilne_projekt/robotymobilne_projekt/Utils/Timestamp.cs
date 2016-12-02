using System;

namespace robotymobilne_projekt.Utils
{
    public static class TimestampUtilities
    {
        public static string Datestamp
        {
            get
            {
                return string.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now);
            }
        }

        public static string Timestamp
        {
            get
            {
                return string.Format("{0:HH:mm:ss}", DateTime.Now);
            }
        }

        public static string FileDatestamp
        {
            get
            {
                return string.Format("{0:dd-MM-yyyy HH-mm-ss}", DateTime.Now);
            }
        }
    }
}
