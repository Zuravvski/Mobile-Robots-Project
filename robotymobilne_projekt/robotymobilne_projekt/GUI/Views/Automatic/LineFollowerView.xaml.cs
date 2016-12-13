using System.Windows.Controls;
using robotymobilne_projekt.GUI.ViewModels;
using robotymobilne_projekt.GUI.ViewModels.Automatic;

namespace robotymobilne_projekt.GUI.Views.Automatic
{
    /// <summary>
    /// Interaction logic for LineFollowerView.xaml
    /// </summary>
    public partial class LineFollowerView : UserControl
    {
        public LineFollowerView()
        {
           InitializeComponent();
           DataContext = new LineFollowerViewModel();
        }
    }
}
