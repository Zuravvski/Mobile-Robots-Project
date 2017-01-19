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

        public CoverPanel(Canvas parent, string buttonText, string textBlockText)
        {
            InitializeComponent();
            this.parent = parent;
            this.buttonText = buttonText;
            this.textBlockText = textBlockText;

            button.Content = buttonText;
            textBlock.Text = textBlockText;
            Height = parent.Height;
            Width = parent.Width;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
            ApplicationService.Instance.AppMode = ApplicationService.ApplicationMode.SERVER;
        }
    }
}
