using System.Windows.Controls;
using robotymobilne_projekt.GUI.ViewModels;

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
