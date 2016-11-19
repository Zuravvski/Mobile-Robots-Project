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

namespace MobileRobots.GUI.Views
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();
            textblock_about.Text = "This is a simple application to controll Popolu 3π robot. It was created as a university project at " +
                "West Pomeranian University of Technology. Our task was to build an app that could exchange data with mobile robot." +
                "\n\nThe main assumptions were : " +
                "\n\tRobots are placed on a board monitored by camera hung on the ceiling" +
                "\n\tCamera tracks robots' positions" +
                "\n\tUser can select between two modes of control: Manual and Automatic" +

                "\n\n\n\nAuthors:" +
                "\n\tAdam Baniuszewicz" +
                "\n\tBartosz Flis" +
                "\n\tMichał Żurawski";
        }
    }
}
