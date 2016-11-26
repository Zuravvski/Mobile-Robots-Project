using System.Windows.Media;

namespace robotymobilne_projekt.Utils.AppLogger
{
    class ErrorLevel : LogLevel
    {
        public ErrorLevel(Logger logger) : base(logger)
        {
            _intValue = LogLevel.ERROR;
            _stringValue = "ERROR";
        }

        public override string calculate(int level, string log)
        {
            if (null != logger)
            {
                if (LogLevel.INFO == level)
                {
                    logger.Foreground = Brushes.Red;
                }
            }
            return _stringValue;
        }
    }
}
