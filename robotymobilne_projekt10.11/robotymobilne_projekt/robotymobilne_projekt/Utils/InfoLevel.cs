using MobileRobots.Utils;
using System.Windows.Media;

namespace robotymobilne_projekt.Utils
{
    class InfoLevel : LogLevel
    {
        public InfoLevel(Logger logger) : base(logger)
        {
            _intValue = LogLevel.INFO;
            _stringValue = "INFO";
        }

        public override string calculate(int level, string log)
        {
            string result = "";
            if (null != logger)
            {
                if (LogLevel.INFO == level)
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
