using MobileRobots;
using MobileRobots.Manual;
using robotymobilne_projekt.Settings;
using System.Collections.Generic;
using System.Windows.Input;

namespace robotymobilne_projekt.GUI.ViewModels
{
    class RobotViewModel : ViewModel
    {
        private ICommand connect;
        private ICommand disconnect;
        private ICommand delete;

        #region Setters & Getters
        public List<RobotModel> ROBOTS
        {
            get
            {
                return RobotSettings.INSTANCE.AVAILABLE_ROBOTS;
            }
        }
        public List<AbstractController> CONTROLLERS
        {
            get
            {
                return ControllerSettings.INSTANCE.AVAILABLE_CONTROLLERS;
            }
        }
        public RobotModel ROBOT { set; get; }
        public bool CONNECTED
        {
            get
            {
                if(null != ROBOT)
                {
                    return ROBOT.CONNECTED;
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
                        if (null != ROBOT)
                        {
                            ROBOT.connect();
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
                        if (null != ROBOT)
                        {
                            ROBOT.disconnect();
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
                        if (null != ROBOT)
                        {
                            ROBOT.disconnect();
                        }
                    });
                }
                return connect;
            }
        }
        #endregion

        public RobotViewModel()
        {
            ROBOT = new RobotModel("Test", "127.0.0.1", 23);
        }
    }
}
