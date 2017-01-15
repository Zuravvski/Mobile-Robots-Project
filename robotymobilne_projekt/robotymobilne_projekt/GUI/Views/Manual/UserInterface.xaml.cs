using System.Windows.Controls;

namespace robotymobilne_projekt.GUI.Views.Manual
{
    /// <summary>
    /// Interaction logic for UserInterface.xaml
    /// </summary>
    public partial class UserInterface : UserControl
    {
        public UserInterface()
        {
            InitializeComponent();
            var coverPanel = new CoverPanel(canvas, "Switch mode", "This feature requires server mode");
            canvas.Children.Add(coverPanel);
        }
    }
}
