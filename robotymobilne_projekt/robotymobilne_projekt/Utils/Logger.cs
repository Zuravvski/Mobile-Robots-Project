using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace MobileRobots.Utils
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
                Dispatcher.BeginInvoke(new Action<string>((logToAdd) =>
                {
                    // Writing log in log console
                    Text += '[' + generateTimeStamp() + "] " + message + '\n';
                    ((ScrollViewer)Parent).ScrollToBottom();
                }), new Object[] { message });

                // Writing log to file
                fileWriter.WriteLine('[' + generateTimeStamp() + "] " + message);
                fileWriter.Flush();
            }
            catch (Exception)
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
                Dispatcher.BeginInvoke(new Action<string, Exception>((logToAdd, exceptionToNote) =>
                {
                    // Writing log in log console
                    Text += '[' + generateTimeStamp() + "] " + message + '\n' + ex.Message + '\n';
                    ((ScrollViewer)Parent).ScrollToBottom();
                }), new Object[] { message, ex });
                
                // Writing log to file
                fileWriter.WriteLine('[' + generateTimeStamp() + "]");
                fileWriter.WriteLine(ex + " " + ex.StackTrace);
                fileWriter.WriteLine();
                fileWriter.Flush();
            }
            catch(Exception)
            {
                MessageBox.Show("An error has occured during writing log to file. Closing file.", "IO Exception");
                fileWriter.Close();
            }
        }
    }
}
