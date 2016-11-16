using MobileRobots.Utils;

namespace robotymobilne_projekt.Utils
{
    abstract class LogLevel
    {
        protected LogLevel nextLevel;
        protected Logger logger;
        protected string _stringValue;
        protected int _intValue;
        
        public static readonly int INFO = 0;
        public static readonly int WARNING = 1;
        public static readonly int ERROR = 2;

        public LogLevel(Logger logger)
        {
            this.logger = logger;
        }

        public void setNextLevel(LogLevel nextLevel)
        {
            this.nextLevel = nextLevel;
        }

        public int intValue()
        {
            return _intValue;
        }

        public string getName()
        {
            return _stringValue;
        }

        public abstract string calculate(int level, string log);
    }
}
