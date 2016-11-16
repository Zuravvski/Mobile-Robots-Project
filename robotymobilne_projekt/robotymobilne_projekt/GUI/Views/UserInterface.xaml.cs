using MobileRobots.Manual;
using robotymobilne_projekt.Settings;
using System.Windows.Controls;
using System.Windows.Media;

namespace robotymobilne_projekt.GUI.Views
{
    /// <summary>
    /// Interaction logic for UserInterface.xaml
    /// </summary>
    public partial class UserInterface : UserControl
    {
        public UserInterface()
        {
            InitializeComponent();
        }

        private void comboBoxControllers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AbstractController selectedController = (AbstractController)comboBoxControllers.SelectedItem;
            if (null != selectedController)
            {
                ControllerSettings.INSTANCE.reserveController(selectedController);
            }
        }
    }
}
