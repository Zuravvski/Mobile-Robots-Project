using robotymobilne_projekt.GUI.ViewModels;
using robotymobilne_projekt.GUI.Views;
using System.Windows.Controls;
using System.Windows.Media;

namespace robotymobilne_projekt.GUI.Pages.Manual
{
    /// <summary>
    /// Interaction logic for ManuaView.xaml
    /// </summary>
    public partial class ManualView : Page
    {
        public ManualView()
        {
            InitializeComponent();
            ManualViewModel model = new ManualViewModel();
            DataContext = model;

            foreach(UserInterface ui in model.USERS)
            {
                userPanel.Children.Add(ui);
            }
        }
    }
}
