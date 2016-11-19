using robotymobilne_projekt.GUI.ViewModels;
using robotymobilne_projekt.GUI.Views;
using System.Windows.Controls;
using System.Windows.Media;

namespace robotymobilne_projekt.GUI.Views.Manual
{
    /// <summary>
    /// Interaction logic for ManuaView.xaml
    /// </summary>
    public partial class ManualView : Page
    {
        private readonly ManualViewModel model;
        public ManualView()
        {
            InitializeComponent();
            model = new ManualViewModel();
            DataContext = model;
        }
    }
}
