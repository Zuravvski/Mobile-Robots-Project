using System.Windows.Media;

namespace robotymobilne_projekt.Utils.AppLogger
{
    class WarningLevel : LogLevel
    {
        public WarningLevel(Logger logger) : base(logger)
        {
            _intValue = WARNING;
            _stringValue = "WARNING";
        }

        public override string calculate(int level, string log)
        {
            string result = _stringValue;
            if (null != logger)
            {
                if (INFO == level)
                {
                    logger.Foreground = Brushes.Orange;
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
