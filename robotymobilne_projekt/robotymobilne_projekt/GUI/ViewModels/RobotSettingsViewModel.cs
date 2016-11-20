using robotymobilne_projekt.GUI.ViewModels;
using robotymobilne_projekt.Settings;
using System;
using System.Windows.Input;

namespace robotymobilne_projekt.GUI.Views.Settings
{
    public class RobotSettingsViewModel : ViewModel
    {
        ICommand reset;
        ICommand incrementAttempts;
        ICommand decrementAttempts;

        public RobotSettings Settings
        {
            get
            {
                return RobotSettings.Instance;
            }
        }

        #region Actions
        public ICommand Reset
        {
            get
            {
                if (null == reset)
                {
                    reset = new DelegateCommand(delegate ()
                    {
                        Settings.reset();
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
                    incrementAttempts = new DelegateCommand(delegate ()
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
                    decrementAttempts = new DelegateCommand(delegate ()
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
