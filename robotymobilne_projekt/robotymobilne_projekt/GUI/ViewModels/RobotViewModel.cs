using robotymobilne_projekt.Devices.Network_utils;
using robotymobilne_projekt.Settings;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using robotymobilne_projekt.Devices;
using robotymobilne_projekt.Manual;

namespace robotymobilne_projekt.GUI.ViewModels
{
    class RobotViewModel : ViewModel, IObserver
    {
        private ICommand connect;
        private ICommand disconnect;
        private ICommand delete;
        private ManualViewModel context;

        // Collections
        private ObservableCollection<AbstractController> controllers;

        // Currently selected
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
                return controllers;
            }
            set
            {
                controllers = value;
                NotifyPropertyChanged("Controllers");
            }
        }

        // Accessors
        public AbstractController Controller
        {
            get
            {
                return controller;
            }
            set
            {
                if (null != value)
                {
                    controller = value;
                }
                else
                {
                    controller = ControllerSettings.Instance.Controllers[0]; // Null object element (NONE)
                }
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
                    connect = new DelegateCommand(delegate
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
                    disconnect = new DelegateCommand(delegate
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
                    delete = new DelegateCommand(delegate
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
            Controllers = new ObservableCollection<AbstractController>(ControllerSettings.Instance.Controllers);
            ControllerSettings.Instance.registerObserver(this);
        }

        public void notify()
        {
            ObservableCollection<AbstractController> refreshedControllers =
                    new ObservableCollection<AbstractController>(ControllerSettings.Instance.Controllers);
            if (null != controller && !controllers.Contains(controller))
            {
                refreshedControllers.Add(controller);
            }
            Controllers = refreshedControllers;
        }
    }
}
