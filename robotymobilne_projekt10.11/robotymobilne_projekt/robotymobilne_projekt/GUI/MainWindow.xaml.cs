using FirstFloor.ModernUI.Windows.Controls;
using MobileRobots.Manual;

namespace MobileRobots
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            RobotModel robot = new RobotModel("elo", "192.168.0.136", 8000);
            AbstractController controller = new GamepadController(0);
            controller.ROBOT = robot;
            robot.CONTROLLER = controller;
            robot.connect();
            robot.run();
        }
        
    }
}
