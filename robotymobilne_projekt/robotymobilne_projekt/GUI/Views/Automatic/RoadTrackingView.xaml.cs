using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using robotymobilne_projekt.GUI.ViewModels.Automatic;
using System;
using System.Collections.Generic;

namespace robotymobilne_projekt.GUI.Views.Automatic
{
    /// <summary>
    /// Interaction logic for RoadTrackingView.xaml
    /// </summary>
    public partial class RoadTrackingView : UserControl
    {
        private Point currentPoint;
        private bool isPainted;

        private double Xactual = 0;
        private double Yactual = 0;
        private double XYdifference = 5;    //sensivity of line sampling
        List<Point> points = new List<Point>(); //to store all coordinates

        public RoadTrackingView()
        {
            InitializeComponent();
            DataContext = new RoadTrackingViewModel();
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


            //if distance in straight line from previous point is bigger than XYdifference
            if (Math.Sqrt(Math.Abs(currentPoint.X - Xactual) + Math.Abs(currentPoint.Y - Yactual)) > XYdifference)
            {
                points.Add(currentPoint);   //add current point to list of coordinates
                Xactual = currentPoint.X;
                Yactual = currentPoint.Y;

                #region ellipse drawing
                //this can (and should be) removed/commented-out later on - its usefull only for setting XYdifference sensivity
                Ellipse ellipse = new Ellipse()
                {
                    Height = 5,
                    Width = 5,
                    StrokeThickness = 1,
                    Stroke = System.Windows.Media.Brushes.Red,
                };

                canvasBoard.Children.Add(ellipse);  //draw ellipse

                Canvas.SetTop(ellipse, Yactual - ellipse.Height / 2); //move ellipse (Y) to new point position
                Canvas.SetLeft(ellipse, Xactual - ellipse.Width / 2); //move ellipse (X) to new point position

                //end of comment/remove
                #endregion      
            }

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

        private void clean_Click(object sender, RoutedEventArgs e)
        {
            canvasBoard.Children.Clear();
            points.Clear();
            isPainted = false;
        }
    }
}
