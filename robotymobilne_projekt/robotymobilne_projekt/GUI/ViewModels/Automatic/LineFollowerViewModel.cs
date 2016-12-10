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
            var hardcodedRobot = RobotSettings.Instance.Robots[1]; // ID: 30
            lineFollower = new LineFollower();

            if (hardcodedRobot.Status == RemoteDevice.StatusE.DISCONNECTED)
            {
                hardcodedRobot.connect();
                driver = new LineFollowerDriver(hardcodedRobot, lineFollower);
            }
        }
       
    }
}