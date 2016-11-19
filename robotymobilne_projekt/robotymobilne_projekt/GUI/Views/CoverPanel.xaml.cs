using System.Windows;
using System.Windows.Controls;

namespace MobileRobots.GUI.Views
{
    /// <summary>
    /// Interaction logic for CoverPanel.xaml
    /// </summary>
    public partial class CoverPanel : UserControl
    {
        public CoverPanel()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
