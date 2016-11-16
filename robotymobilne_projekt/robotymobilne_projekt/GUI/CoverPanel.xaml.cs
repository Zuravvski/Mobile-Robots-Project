using System.Windows;
using System.Windows.Controls;

namespace robotymobilne_projekt.GUI
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
