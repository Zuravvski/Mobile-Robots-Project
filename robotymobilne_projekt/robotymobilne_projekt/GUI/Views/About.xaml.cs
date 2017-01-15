using System.Windows.Controls;

namespace robotymobilne_projekt.GUI.Views
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();
            textblock_about.Text = "This is a simple application to controll Popolu 3π robot. \nThis was created as a university project at " +
                "West Pomeranian University of Technology. \nOur task was to build an app that could exchange data with mobile robot." +
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
