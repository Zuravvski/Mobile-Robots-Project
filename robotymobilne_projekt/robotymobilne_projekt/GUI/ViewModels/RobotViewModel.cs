using robotymobilne_projekt.Settings;
using System.Collections.ObjectModel;
using System.Windows.Input;
using robotymobilne_projekt.Devices;
using robotymobilne_projekt.Manual;

namespace robotymobilne_projekt.GUI.ViewModels
{
    public abstract class RobotViewModel : ViewModel
    {
        protected ICommand connect;
        protected ICommand disconnect;

        // Currently selected
        protected RobotModel robot;
        protected AbstractController controller;
        protected RobotDriver driver;

        #region Setters & Getters

        public ObservableCollection<RobotModel> Robots
        {
            get
            {
                return RobotSettings.Instance.Robots;
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

        public abstract ICommand Connect { get; }
        
        public abstract ICommand Disconnect { get; }
        #endregion
    }
}
