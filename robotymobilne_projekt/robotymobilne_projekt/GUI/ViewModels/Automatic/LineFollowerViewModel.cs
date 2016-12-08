using robotymobilne_projekt.Automatic;
using robotymobilne_projekt.Devices;

namespace robotymobilne_projekt.GUI.ViewModels
{
    public class LineFollowerViewModel : ViewModel
    {
        public RobotModel Robot { get; set; }
        public LineFollower LineFollower { get; set; }

        private LineFollowerDriver driver;

        public LineFollowerViewModel(RobotModel robot, LineFollower lineFollower)
        {
            // TODO: Definitely remove this ridiculous constructor
            Robot = robot;
            LineFollower = lineFollower;
            driver = new LineFollowerDriver(robot, lineFollower);
        }

        
    }
}