using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using robotymobilne_projekt.Settings;

namespace robotymobilne_projekt.GUI.Views
{
    /// <summary>
    /// Interaction logic for CoverPanel.xaml
    /// </summary>
    public partial class CoverPanel : UserControl
    {
        private string buttonText;
        private string textBlockText;
        private Canvas parent;

        public CoverPanel()
        {
            InitializeComponent();
            button.Content = "Switch Mode";
            textBlock.Text = "This feature requires server mode";
        }

        //public CoverPanel(Canvas parent, string buttonText, string textBlockText)
        //{
        //    InitializeComponent();
        //    this.parent = parent;
        //    this.buttonText = buttonText;
        //    this.textBlockText = textBlockText;

        //    button.Content = buttonText;
        //    textBlock.Text = textBlockText;
        //}

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var loader = new ProgressBarLoader();
            try
            {
                loader.ShowDialog();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
