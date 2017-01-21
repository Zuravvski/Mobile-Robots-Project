using System.Windows.Controls;
using robotymobilne_projekt.GUI.ViewModels.Automatic;

namespace robotymobilne_projekt.GUI.Views.Automatic
{
    /// <summary>
    /// Interaction logic for LineFollowerView.xaml
    /// </summary>
    public partial class AutomaticView : UserControl
    {
        public AutomaticView()
        {
            InitializeComponent();
            DataContext = new AutomaticViewModel();

            viewboxModeArea.Child = new CoverPanelLoading();
        }
    }
}
