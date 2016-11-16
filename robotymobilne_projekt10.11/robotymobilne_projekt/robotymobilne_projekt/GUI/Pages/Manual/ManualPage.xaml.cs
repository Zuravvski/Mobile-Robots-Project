using MobileRobots.Manual;
using robotymobilne_projekt.GUI.Views;
using robotymobilne_projekt.Manual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace robotymobilne_projekt.GUI.Pages.Manual
{
    /// <summary>
    /// Interaction logic for UserInterface.xaml
    /// </summary>
    public partial class ManualPage : UserControl
    {
        public ManualPage()
        {
            InitializeComponent();

            adduser();
            adduser();
            adduser();
            adduser();

        }

        private void adduser()
        {
            Viewbox viewbox = new Viewbox();
            viewbox.Child = new UserInterface();
            UniformGrid_ManualPage.Children.Add(viewbox);
        }
    }
}
