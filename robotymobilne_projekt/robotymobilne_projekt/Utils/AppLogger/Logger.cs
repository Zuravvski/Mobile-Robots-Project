using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace robotymobilne_projekt.Utils.AppLogger
{
    public class Logger : TextBlock
    {
        private static readonly Lazy<Logger> instance = new Lazy<Logger>(() => new Logger());
        private readonly StreamWriter fileWriter;
        private LogLevel infoLevel;
        private LogLevel warningLevel;
        private LogLevel errorLevel;
        private ScrollViewer parentScrollPane;

        public static Logger Instance
        {
            get { return instance.Value; }
        }

        public ScrollViewer PARENT
        {
            get
            {
                return parentScrollPane;
            }
            set
            {
                parentScrollPane = value;
            }
        }

        private Logger()
        {
            // Look and feel settings 
            Padding = new Thickness(5, 5, 5, 5);
            TextWrapping = TextWrapping.Wrap;

            // Creating file handler for logs
            if (!Directory.Exists("Logs"))
            {
                Directory.CreateDirectory("Logs");
            }
            fileWriter = new StreamWriter("Logs\\log " + TimestampUtilities.FileDatestamp + ".txt");
            

            // Creating logger's chain of responsibility
            infoLevel = new InfoLevel(this);
            warningLevel = new WarningLevel(this);
            errorLevel = new ErrorLevel(this);

            infoLevel.setNextLevel(warningLevel);
            warningLevel.setNextLevel(errorLevel);
        }

        public void log(int level, string message)
        {
            try
            {
                Dispatcher.BeginInvoke(new Action<int, string>((lvl, logToAdd) =>
                {
                    var levelName = infoLevel.calculate(level, message);
                    // Writing log in log console
                    Text += '[' + TimestampUtilities.Datestamp + "] " + message + '\n';

                    parentScrollPane?.ScrollToBottom();

                    // Writing log to file
                    fileWriter.WriteLine('[' + levelName + "][" + TimestampUtilities.Datestamp + "] " + message);
                    fileWriter.Flush();
                }), new Object[] { level, message });
            }
            catch (Exception)
            {
                fileWriter.Close();
            }
        }

        public void log(int level, string message, Exception ex)
        {
            try
            {
                Dispatcher.BeginInvoke(new Action<int, string, Exception>((lvl, logToAdd, exceptionToNote) =>
                {
                    var levelName = infoLevel.calculate(level, message);
                    // Writing log in log console
                    Text += '[' + TimestampUtilities.Datestamp + "] " + message + '\n' + ex.Message + '\n';
                    parentScrollPane?.ScrollToBottom();

                    // Writing log to file
                    fileWriter.WriteLine('[' + levelName + "][" + TimestampUtilities.Datestamp + "]");
                    fileWriter.WriteLine(ex + " " + ex.StackTrace);
                    fileWriter.WriteLine();
                    fileWriter.Flush();
                }), new Object[] { level, message, ex });
            }
            catch(Exception)
            {
                MessageBox.Show("An error has occured during writing log to file. Closing file.", "IO Exception");
                fileWriter.Close();
            }
        }
    }
}
