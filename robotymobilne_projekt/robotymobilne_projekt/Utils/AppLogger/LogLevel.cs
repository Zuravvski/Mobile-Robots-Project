

namespace robotymobilne_projekt.Utils.AppLogger
{
    abstract class LogLevel
    {
        protected LogLevel nextLevel;
        protected Logger logger;
        protected string _stringValue;
        protected int _intValue;

        public const int INFO = 0;
        public const int WARNING = 1;
        public const int ERROR = 2;

        protected LogLevel(Logger logger)
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
