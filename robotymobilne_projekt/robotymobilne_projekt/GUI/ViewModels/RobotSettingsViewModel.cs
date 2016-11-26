using System.Windows.Input;
using robotymobilne_projekt.Settings;

namespace robotymobilne_projekt.GUI.ViewModels
{
    public class RobotSettingsViewModel : ViewModel
    {
        private ICommand reset;
        private ICommand incrementAttempts;
        private ICommand decrementAttempts;

        public RobotSettings Settings => RobotSettings.Instance;

        #region Actions
        public ICommand Reset
        {
            get
            {
                if (null == reset)
                {
                    reset = new DelegateCommand(delegate 
                    {
                        RobotSettings.Instance.reset();
                    });
                }
                return reset;
            }
        }
        public ICommand IncrementAttempts
        {
            get
            {
                if (null == incrementAttempts)
                {
                    incrementAttempts = new DelegateCommand(delegate 
                    {  
                        RobotSettings.Instance.ReconnectAttempts++;
                    });
                }
                return incrementAttempts;
            }
        }
        public ICommand DecrementAttempts
        {
            get
            {
                if (null == decrementAttempts)
                {
                    decrementAttempts = new DelegateCommand(delegate
                    {
                        RobotSettings.Instance.ReconnectAttempts--;
                    });
                }
                return decrementAttempts;
            }
        }
        #endregion
    }
}
