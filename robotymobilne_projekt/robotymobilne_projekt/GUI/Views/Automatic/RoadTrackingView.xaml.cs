using System.Diagnostics;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using robotymobilne_projekt.GUI.ViewModels.Automatic;

namespace robotymobilne_projekt.GUI.Views.Automatic
{
    /// <summary>
    /// Interaction logic for RoadTrackingView.xaml
    /// </summary>
    public partial class RoadTrackingView : UserControl
    {
        private Point currentPoint;
        private bool isPainted;

        public RoadTrackingView()
        {
            InitializeComponent();
            DataContext = new LineFollowerViewModel();
        }

        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed || isPainted) return;
            var line = new Line
            {
                Stroke = SystemColors.WindowFrameBrush,
                X1 = currentPoint.X,
                Y1 = currentPoint.Y,
                X2 = e.GetPosition(this).X,
                Y2 = e.GetPosition(this).Y
            };

            currentPoint = e.GetPosition(this);
            canvasBoard.Children.Add(line);
        }

        private void canvasBoard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                currentPoint = e.GetPosition(this);
            }
        }

        private void canvasBoard_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isPainted = true;
        }
    }
}
