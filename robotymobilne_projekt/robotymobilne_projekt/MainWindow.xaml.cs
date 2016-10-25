using robotymobilne_projekt.Manual;
using robotymobilne_projekt.Utils;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace robotymobilne_projekt
{
    public partial class MainWindow : Window
    {
        private List<RobotModel> robots;
        private readonly Logger logger = Logger.getLogger();
        IController currentController;

        public MainWindow()
        {
            InitializeComponent();
            populateListWithPredefinedRobots();
            scrollViewerLogger.Content = logger;
            currentController = new KeyboardController(this);
            ((KeyboardController)currentController).ROBOT = robots[0];
            new AddGamepad();
        }

        private void populateListWithPredefinedRobots()
        {
            robots = new List<RobotModel>()
            {
                new RobotModel("30", "192.168.2.30", 8000),
                new RobotModel("31", "192.168.2.31", 8000),
                new RobotModel("32", "192.168.2.32", 8000),
                new RobotModel("33", "192.168.2.33", 8000),
                new RobotModel("34", "192.168.2.34", 8000),
            };
            list_availabledevices.ItemsSource = robots;
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
            RobotModel selectedRobot = (RobotModel)list_availabledevices.SelectedItem;
            if (null != selectedRobot)
            {
                bool result = selectedRobot.connect();
                if (result)
                {
                    button_connectdevices.IsEnabled = false;
                }
            }
        }

        private void button_disconnectdevices_Click(object sender, RoutedEventArgs e)
        {
            RobotModel selectedRobot = (RobotModel)list_availabledevices.SelectedItem;
            if (null != selectedRobot && selectedRobot.isConnected())
            {
                selectedRobot.disconnect();
                button_connectdevices.IsEnabled = true;
            }
        }

        private void button_send_Click(object sender, RoutedEventArgs e)
        {
            robots[0].sendDataFrame("000000");
        }
    }
}
