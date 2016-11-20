using MobileRobots;
using MobileRobots.Manual;
using robotymobilne_projekt.Settings;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace robotymobilne_projekt.GUI.ViewModels
{
    class RobotViewModel : ViewModel
    {
        private ICommand connect;
        private ICommand disconnect;
        private ICommand delete;
        private ManualViewModel context;

        #region Setters & Getters
        public ObservableCollection<RobotModel> Robots
        {
            get
            {
                return RobotSettings.Instance.AvailableRobots;
            }
        }
        public List<AbstractController> Controllers
        {
            get
            {
                return ControllerSettings.Instance.AVAILABLE_CONTROLLERS;
            }
        }
        public RobotModel Robot { set; get; }
        public bool Connected
        {
            get
            {
                if(null != Robot)
                {
                    return Robot.Connected;
                }
                return false;
            }
        }
        #endregion

        #region Actions
        public ICommand Connect
        {
            get
            {
                if(null == connect)
                {
                    connect = new DelegateCommand(delegate ()
                    {
                        if (null != Robot)
                        {
                            Robot.connect();
                        }
                    });
                }
                return connect;
            }
        }

        public ICommand Disconnect
        {
            get
            {
                if (null == disconnect)
                {
                    disconnect = new DelegateCommand(delegate ()
                    {
                        if (null != Robot)
                        {
                            Robot.disconnect();
                        }
                    });
                }
                return disconnect;
            }
        }

        // TODO: Handle deletion
        public ICommand Delete
        {
            get
            {
                if (null == delete)
                {
                    delete = new DelegateCommand(delegate ()
                    {
                        if (null != Robot && Robot.Connected)
                        {
                            Robot.disconnect();
                        }
                        context.RemoveUser.Execute(this);
                    });
                }
                return delete;
            }
        }
        #endregion

        public RobotViewModel(ManualViewModel context)
        {
            this.context = context;
            Robot = new RobotModel("Test", "127.0.0.1", 23);
        }
    }
}
