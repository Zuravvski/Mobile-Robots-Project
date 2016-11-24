using System.Windows.Media;

namespace robotymobilne_projekt.Utils.AppLogger
{
    class InfoLevel : LogLevel
    {
        public InfoLevel(Logger logger) : base(logger)
        {
            _intValue = INFO;
            _stringValue = "INFO";
        }

        public override string calculate(int level, string log)
        {
            string result = "";
            if (null != logger)
            {
                if (INFO == level)
                {
                    logger.Foreground = Brushes.Black;
                    result = _stringValue;
                }
                else
                {
                    result = nextLevel.calculate(level, log);
                }
            }
            return result;
        }
    }
}
