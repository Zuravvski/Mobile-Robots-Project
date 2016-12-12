using System.Collections.ObjectModel;
using robotymobilne_projekt.Automatic;
using robotymobilne_projekt.Devices;
using robotymobilne_projekt.Settings;

namespace robotymobilne_projekt.GUI.ViewModels
{
    public class LineFollowerViewModel : ViewModel
    {
        private LineFollower lineFollower;

        public RobotModel Robot { get; set; }

        public LineFollower LineFollower
        {
            get
            {
                return lineFollower;
            }
            set
            {
                lineFollower = value;
                NotifyPropertyChanged("LineFollower");
            }
        }

        private LineFollowerDriver driver;

        public LineFollowerViewModel()
        {
            LineFollower = new LineFollower {Sensors = new ObservableCollection<int>() {0, 0, 0, 0, 0}};

            // Hardcode for testing purpose
            Robot = RobotSettings.Instance.Robots[2]; // ID: 30
            //LineFollower = new LineFollower();

            if (Robot.Status == RemoteDevice.StatusE.DISCONNECTED)
            {
                Robot.connect();
                driver = new LineFollowerDriver(Robot, LineFollower);
            }
        }
       
    }
}