using MobileRobots.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace robotymobilne_projekt.Utils
{
    class WarningLevel : LogLevel
    {
        public WarningLevel(Logger logger) : base(logger)
        {
            _intValue = LogLevel.WARNING;
            _stringValue = "WARNING";
        }

        public override string calculate(int level, string log)
        {
            string result = "";
            if (null != logger)
            {
                if (LogLevel.INFO == level)
                {
                    logger.Foreground = Brushes.Orange;
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
