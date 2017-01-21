using System.Windows.Controls;
using robotymobilne_projekt.GUI.ViewModels;

namespace robotymobilne_projekt.GUI.Views.Settings
{
    /// <summary>
    /// Interaction logic for Appearance.xaml
    /// </summary>
    public partial class Appearance : UserControl
    {
        public Appearance()
        {
            InitializeComponent();

            // create and assign the appearance view model
            var model = new AppearanceViewModel();
            this.DataContext = model;
            CoverPanelLoading.viewModel = model;
        }
    }
}
