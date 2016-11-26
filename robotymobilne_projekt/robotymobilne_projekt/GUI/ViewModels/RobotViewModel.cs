using System;
using robotymobilne_projekt.Devices.Network_utils;
using robotymobilne_projekt.Settings;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using robotymobilne_projekt.Devices;
using robotymobilne_projekt.Manual;

namespace robotymobilne_projekt.GUI.ViewModels
{
    public class RobotViewModel : ViewModel
    {
        private ICommand connect;
        private ICommand disconnect;
        private ICommand delete;
        private readonly ManualViewModel context;

        // Currently selected
        private RobotModel robot;
        private AbstractController controller;
        private ManualDriver driver;

        #region Setters & Getters

        public ObservableCollection<RobotModel> Robots
        {
            get
            {
                ObservableCollection<RobotModel> filteredRobots =
                    new ObservableCollection<RobotModel>(RobotSettings.Instance.Robots);
                if (null != robot && !filteredRobots.Contains(robot))
                {
                    filteredRobots.Add(robot);
                }
                return filteredRobots;
            }
        }

        public ObservableCollection<AbstractController> Controllers
        {
            get { return ControllerSettings.Instance.Controllers; }
        }

        // Accessors
        public AbstractController Controller
        {
            get { return controller; }
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
            get { return robot; }
            set
            {
                robot = value;
                NotifyPropertyChanged("Robot");
            }
        }

        #endregion

        #region Actions

        public ICommand Connect
        {
            get
            {
                if (null == connect)
                {
                    connect = new DelegateCommand(delegate
                    {
                        try
                        {
                            if (robot.Status != RemoteDevice.StatusE.CONNECTED)
                            {
                                Robot.connect();
                                driver = new ManualDriver(robot, controller);
                            }
                        }
                        catch
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
                        if (Robot != null && Robot.Status == RemoteDevice.StatusE.CONNECTED)
                        {
                            Robot.disconnect();
                        }
                    });
                }
                return disconnect;
            }
        }

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
                            driver.Dispose();
                            Robot = null;
                            Controller = null;
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
