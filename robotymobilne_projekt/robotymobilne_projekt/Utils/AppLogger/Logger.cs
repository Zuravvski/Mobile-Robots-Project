using robotymobilne_projekt.Utils;
using robotymobilne_projekt.Utils.AppLogger;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace MobileRobots.Utils.AppLogger
{
    class Logger : TextBlock
    {
        public static Logger INSTANCE = null;
        private StreamWriter fileWriter;
        private LogLevel infoLevel;
        private LogLevel warningLevel;
        private LogLevel errorLevel;
        private ScrollViewer parentScrollPane;

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

        private Logger() : base()
        {
            // Look and feel settings 
            Padding = new Thickness(5, 5, 5, 5);
            TextWrapping = TextWrapping.Wrap;

            // Creating file handler for logs
            fileWriter = new StreamWriter("log " + TimestampUtilities.FileDatestamp + ".txt");

            // Creating logger's chain of responsibility
            infoLevel = new InfoLevel(this);
            warningLevel = new WarningLevel(this);
            errorLevel = new ErrorLevel(this);

            infoLevel.setNextLevel(warningLevel);
            warningLevel.setNextLevel(errorLevel);
        }

        public static Logger getLogger()
        {
            if(null == INSTANCE)
            {
                INSTANCE = new Logger();
            }
            return INSTANCE;
        }

        public void log(int level, string message)
        {
            try
            {
                Dispatcher.BeginInvoke(new Action<int, string>((lvl, logToAdd) =>
                {
                    // TODO: Add [LEVEL] tag in file to make everything more selective
                    string levelName = infoLevel.calculate(level, message);
                    // Writing log in log console
                    Text += '[' + TimestampUtilities.Datestamp + "] " + message + '\n';

                    if (null != parentScrollPane)
                    {
                        parentScrollPane.ScrollToBottom();
                    }
                }), new Object[] { level, message });

                // Writing log to file
                fileWriter.WriteLine('[' + TimestampUtilities.Datestamp + "] " + message);
                fileWriter.Flush();
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
                    string levelName = infoLevel.calculate(level, message);
                    // Writing log in log console
                    Text += '[' + TimestampUtilities.Datestamp + "] " + message + '\n' + ex.Message + '\n';
                    if (null != parentScrollPane)
                    {
                        parentScrollPane.ScrollToBottom();
                    }
                }), new Object[] { level, message, ex });
                
                // Writing log to file
                fileWriter.WriteLine('[' + TimestampUtilities.Datestamp + "]");
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
