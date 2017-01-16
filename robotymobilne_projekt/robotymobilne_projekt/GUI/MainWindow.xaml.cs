using FirstFloor.ModernUI.Windows.Controls;
using robotymobilne_projekt.GUI.ViewModels;

namespace robotymobilne_projekt.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void ModernWindow_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.Tab:
                case System.Windows.Input.Key.Up:
                case System.Windows.Input.Key.Down:
                case System.Windows.Input.Key.Left:
                case System.Windows.Input.Key.Right:
                    e.Handled = true;
                    break;
            }
        }
    }
}
