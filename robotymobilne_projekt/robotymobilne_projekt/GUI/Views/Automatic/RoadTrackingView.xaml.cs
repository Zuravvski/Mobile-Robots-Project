using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using robotymobilne_projekt.GUI.ViewModels.Automatic;
using System;
using System.Collections.Generic;
using FirstFloor.ModernUI.Presentation;
using System.Windows.Media;
using Microsoft.Win32;
using System.IO;
using System.Xml.Serialization;
using static robotymobilne_projekt.Settings.ApplicationService;

namespace robotymobilne_projekt.GUI.Views.Automatic
{
    /// <summary>
    /// Interaction logic for RoadTrackingView.xaml
    /// </summary>
    public partial class RoadTrackingView : UserControl
    {
        private Point currentPoint;
        private readonly RoadTrackingViewModel roadTrackingViewModel;

        private double Xactual = 0;
        private double Yactual = 0;
        private double XYdifference = 5;    //sensivity of line sampling

        public RoadTrackingView()
        {
            InitializeComponent();
            var viewModel = new RoadTrackingViewModel();
            roadTrackingViewModel = viewModel;
            DataContext = viewModel;
        }

        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!(Instance.AppMode == ApplicationMode.SERVER)) return;
            if (e.LeftButton != MouseButtonState.Pressed || roadTrackingViewModel.IsPainted || roadTrackingViewModel.IsRunning || !(Instance.AppMode == ApplicationMode.SERVER)) return;
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
                roadTrackingViewModel.Points.Add(currentPoint);   //add current point to list of coordinates
                Xactual = currentPoint.X;
                Yactual = currentPoint.Y;

                #region ellipse drawing
                //this can (and should be) removed/commented-out later on - its usefull only for setting XYdifference sensivity
                Ellipse ellipse = new Ellipse()
                {
                    Height = 5,
                    Width = 5,
                    StrokeThickness = 1,
                    Stroke = new SolidColorBrush(AppearanceManager.Current.AccentColor),
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
            if (!(Instance.AppMode == ApplicationMode.SERVER)) return;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                currentPoint = e.GetPosition(this);
            }
        }

        private void canvasBoard_MouseUp(object sender, MouseButtonEventArgs e)
        {
             if (!(Instance.AppMode == ApplicationMode.SERVER)) return;
            roadTrackingViewModel.IsPainted = true;
        }

        private void clean_Click(object sender, RoutedEventArgs e)
        {
            if (!(Instance.AppMode == ApplicationMode.SERVER)) return;
            canvasBoard.Children.Clear();
            roadTrackingViewModel.Points.Clear();
            roadTrackingViewModel.IsPainted = false;
            roadTrackingViewModel.IsRunning = false;
            roadTrackingViewModel.PlayPauseIcon = RoadTrackingViewModel.PLAY_ICON;
        }

        private void buttonLoad_Click(object sender, RoutedEventArgs e)
        {
            if (!(Instance.AppMode == ApplicationMode.SERVER)) return;
            if (!roadTrackingViewModel.IsRunning)
            Deserialize();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (!(Instance.AppMode == ApplicationMode.SERVER)) return;
            Serialize();
        }

        private void Serialize()
        {
            var xmlSer = new XmlSerializer(typeof(List<Point>));
            var dialog = new SaveFileDialog();
            dialog.Filter = "Extensible Markup Language (*.xml)|*.xml";
            dialog.FileName = "Filename.xml"; //set initial filename
            if (dialog.ShowDialog() == true)
            {
                using (var streaming = dialog.OpenFile())
                {
                    xmlSer.Serialize(streaming, roadTrackingViewModel.Points);
                    streaming.Close();
                }
            }
        }

        private void Deserialize()
        {
            var xmlSer = new XmlSerializer(typeof(List<Point>));
            var dialog = new OpenFileDialog();
            dialog.Filter = "eXtensible Markup Language (*.xml)|*.xml";
            dialog.FileName = "Filename.xml"; //set initial filename
            if (dialog.ShowDialog() == true)
            {
                using (var streaming = dialog.OpenFile())
                {
                    try
                    {
                        roadTrackingViewModel.IsPainted = true;
                        roadTrackingViewModel.Points = null;
                        canvasBoard.Children.Clear();
                        StreamReader reader = new StreamReader(streaming);
                        roadTrackingViewModel.Points = (List<Point>)xmlSer.Deserialize(reader);
                        streaming.Close();


                        //update map
                        for (int i = 0; i < roadTrackingViewModel.Points.Count; i++)
                        {

                            Ellipse ellipse = new Ellipse()
                            {
                                Height = 5,
                                Width = 5,
                                StrokeThickness = 1,
                                Stroke = new SolidColorBrush(AppearanceManager.Current.AccentColor),
                            };


                            canvasBoard.Children.Add(ellipse);  //draw ellipse

                            Canvas.SetTop(ellipse, roadTrackingViewModel.Points[i].Y - ellipse.Height / 2); //move ellipse (Y) to new point position
                            Canvas.SetLeft(ellipse, roadTrackingViewModel.Points[i].X - ellipse.Width / 2); //move ellipse (X) to new point position

                            Polyline line = new Polyline();
                            PointCollection collection = new PointCollection();
                            foreach (Point p in roadTrackingViewModel.Points)
                            {
                                collection.Add(p);
                            }
                            line.Points = collection;
                            line.Stroke = SystemColors.WindowFrameBrush;
                            line.StrokeThickness = 0.4;
                            canvasBoard.Children.Add(line);
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        MessageBox.Show("error!");
                        roadTrackingViewModel.IsPainted = false;
                    }
                }
            }
        }
    }
}
