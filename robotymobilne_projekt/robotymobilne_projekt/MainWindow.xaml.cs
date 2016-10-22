using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace robotymobilne_projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<RobotModel> robots;


        public MainWindow()
        {
            InitializeComponent();
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

        private void button_send_Click(object sender, RoutedEventArgs e)
        {
            robots[3].send(textBox_Data2send.Text);
        }

        private void button_stop_Click(object sender, RoutedEventArgs e)
        {

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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    robots[3].send("032D2D");
                    break;
                case Key.S:
                    robots[3].send("030000");
                    robots[3].send("03E2E2");
                    break;
                case Key.A:
                    robots[3].send("031E3C");
                    break;
                case Key.D:
                    robots[3].send("033C1E");
                    break;
                case Key.Space:
                    robots[3].send("030000");
                    break;
                case Key.LeftShift:
                    robots[3].send("035A5A");
                    break;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            robots[3].send("032D2D");
        }
    }
}
