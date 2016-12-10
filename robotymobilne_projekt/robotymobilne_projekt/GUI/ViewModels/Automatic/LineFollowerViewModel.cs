using System.Collections.Generic;
using System.Collections.ObjectModel;
using robotymobilne_projekt.Automatic;
using robotymobilne_projekt.Automatic.LineFollower;
using robotymobilne_projekt.Devices;

namespace robotymobilne_projekt.GUI.ViewModels
{
    public class LineFollowerViewModel : ViewModel
    {
        private LineFollowerController lineFollower;
        private List<LineFollowerAlgorithm.Type> algorithms =
            new List<LineFollowerAlgorithm.Type>() { LineFollowerAlgorithm.Type.P, LineFollowerAlgorithm.Type.PID };
        private LineFollowerAlgorithm.Type currentAlgorithm;
        private readonly LineFollowerAlgorithmFactory algorithmFactory;
        private RobotDriver driver;

        public RobotModel Robot { get; set; }

        public List<LineFollowerAlgorithm.Type> Algorithms
        {
            get { return algorithms; }
        }

        public LineFollowerAlgorithm.Type CurrentAlgorithm
        {
            get { return currentAlgorithm; }
            set
            {
                currentAlgorithm = value;
                lineFollower.Algorithm = algorithmFactory.getAlgorithm(currentAlgorithm);
                NotifyPropertyChanged("CurrentAlgorithm");
            }
        }


        public LineFollowerController LineFollower
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

        public LineFollowerViewModel()
        {
            lineFollower = new LineFollowerController {Sensors = new ObservableCollection<int>() {0, 0, 0, 0, 0}};
            algorithmFactory = new LineFollowerAlgorithmFactory();
            CurrentAlgorithm = LineFollowerAlgorithm.Type.P;
        }   
    }
}