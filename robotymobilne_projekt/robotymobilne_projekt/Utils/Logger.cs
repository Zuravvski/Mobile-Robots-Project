using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace robotymobilne_projekt.Utils
{
    class Logger : TextBlock
    {
        public static Logger INSTANCE = null;
        private StreamWriter fileWriter;

        private Logger() : base()
        {
            // Look and feel settings 
            Padding = new Thickness(5, 5, 5, 5);
            TextWrapping = TextWrapping.Wrap;

            // Creating file handler for logs
            fileWriter = new StreamWriter("log " + generateTimeStampForFile() + ".txt");
        }

        public static Logger getLogger()
        {
            if(null == INSTANCE)
            {
                INSTANCE = new Logger();
            }
            return INSTANCE;
        }

        public void log(string message)
        {
            try
            {
                // Writing log in log console
                Text += '[' + generateTimeStamp() + "] " + message + '\n';
                ((ScrollViewer)Parent).ScrollToBottom();

                // Writing log to file
                fileWriter.WriteLine('[' + generateTimeStamp() + "] " + message);
                fileWriter.Flush();
            }
            catch(Exception)
            {
                fileWriter.Close();
            }
        }

        private string generateTimeStamp()
        {
            return string.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now);
        }

        private string generateTimeStampForFile()
        {
            return string.Format("{0:dd-MM-yyyy HH-mm-ss}", DateTime.Now);
        }

        public void log(string message, Exception ex)
        {
            try
            {
                // Writing log in log console
                Text += '[' + generateTimeStamp() + "] " + message + '\n' + ex.Message + '\n';
                ((ScrollViewer)Parent).ScrollToBottom();

                // Writing log to file
                fileWriter.WriteLine('[' + generateTimeStamp() + "]");
                fileWriter.WriteLine(ex + " " + ex.StackTrace);
                fileWriter.WriteLine();
                fileWriter.Flush();
            }
            catch(Exception)
            {
                fileWriter.Close();
            }
        }
    }
}
