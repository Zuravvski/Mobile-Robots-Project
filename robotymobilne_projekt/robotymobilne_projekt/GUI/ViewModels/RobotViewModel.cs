using MobileRobots;
using MobileRobots.Manual;
using robotymobilne_projekt.Devices.Network_utils;
using robotymobilne_projekt.Settings;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.Windows.Data;

namespace robotymobilne_projekt.GUI.ViewModels
{
    class RobotViewModel : ViewModel
    {
        private ICommand connect;
        private ICommand disconnect;
        private ICommand delete;
        private ManualViewModel context;
        private RobotModel robot;
        private AbstractController controller;

        #region Setters & Getters
        public ObservableCollection<RobotModel> Robots
        {
            get
            {
                ObservableCollection<RobotModel> filteredRobots = new ObservableCollection<RobotModel>(RobotSettings.Instance.Robots);
                if(null != robot && !filteredRobots.Contains(robot))
                {
                    filteredRobots.Add(robot);
                }
                return filteredRobots;
            }
        }
        public ObservableCollection<AbstractController> Controllers
        {
            get
            {
                ObservableCollection<AbstractController> filteredControllers = 
                        new ObservableCollection<AbstractController>(ControllerSettings.Instance.Controllers);
                if (null != controller && !filteredControllers.Contains(controller))
                {
                    filteredControllers.Add(controller);
                }
                return filteredControllers;
            }
        }

        public AbstractController Controller
        {
            get
            {
                return controller;
            }
            set
            {
                controller = value;
                NotifyPropertyChanged("Controller");
            }
        }
        public RobotModel Robot
        {
            get
            {
                return robot;
            }
            set
            {
                robot = value;
                NotifyPropertyChanged("Robot");
            }
        }
        public ManualDriver Driver;
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
                        if (null != Robot && !Robot.DeviceName.Equals("NONE") && 
                            null != Controller && !Controller.ToString().Equals("NONE"))
                        {
                            
                            Robot.connect();
                            Driver = new ManualDriver(robot, controller);
                            
                        }
                        else
                        {
                            MessageBox.Show("Please choose valid robot and controller.", "Invalid settings");
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
                            Robot = null;
                            Controller = null;
                            Driver = null;
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
        }
    }
}
