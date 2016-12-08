using System.Collections.ObjectModel;
using robotymobilne_projekt.Automatic;
using robotymobilne_projekt.Devices;

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
            LineFollower = new LineFollower();
            LineFollower.Sensors = new ObservableCollection<int>() {700, 200, 300, 400, 1500};
        }
       
    }
}