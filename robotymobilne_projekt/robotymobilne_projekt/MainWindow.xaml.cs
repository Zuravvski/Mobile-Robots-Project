using MobileRobots.Manual;
using MobileRobots.Utils;
using OpenTK.Input;
using System.Windows;
using System.Windows.Controls;

namespace MobileRobots
{
    public partial class MainWindow : Window
    {
        private readonly Logger logger = Logger.getLogger();

        public MainWindow()
        {
            InitializeComponent();
            scrollViewerLogger.Content = logger;
        }

        private void list_availabledevices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RobotModel selectedRobot = (RobotModel)list_availabledevices.SelectedItem;
            if (null != selectedRobot)
            {
                label_ipvalue.Content = selectedRobot.IP;
                label_portvalue.Content = selectedRobot.PORT;
            }
        }

        private void button_connectdevices_Click(object sender, RoutedEventArgs e)
        {
            Logger.getLogger().log("Connecting to robotellas");

            // Robot 1
            RobotModel robot1 = new RobotModel("Adam", "192.168.2.30", 8000);
            robot1.CONTROLLER = new GamepadController(0);
            robot1.connect();
            robot1.run();

            // Robot 2
            RobotModel robot2 = new RobotModel("Bartek", "192.168.2.31", 8000);
            robot2.CONTROLLER = new GamepadController(1);
            robot2.connect();
            robot2.run();

            // Robot 3
            RobotModel robot3 = new RobotModel("Michal", "192.168.2.33", 8000);
            KeyboardState key = Keyboard.GetState();
            robot3.CONTROLLER = new KeyboardController(key);
            robot3.connect();
            robot3.run();
        }

        private void button_disconnectdevices_Click(object sender, RoutedEventArgs e)
        {
            RobotModel selectedRobot = (RobotModel)list_availabledevices.SelectedItem;
            if (null != selectedRobot && selectedRobot.TCPCLIENT.Connected)
            {
                selectedRobot.disconnect();
            }
        }
    }
}
