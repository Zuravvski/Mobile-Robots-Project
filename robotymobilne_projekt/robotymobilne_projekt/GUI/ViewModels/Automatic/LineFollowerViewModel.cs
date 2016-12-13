using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using robotymobilne_projekt.Automatic.LineFollower;
using robotymobilne_projekt.Devices;

namespace robotymobilne_projekt.GUI.ViewModels.Automatic
{
    public class LineFollowerViewModel : RobotViewModel
    {
        private LineFollowerController lineFollower;
        private LineFollowerAlgorithm.Type currentAlgorithm;
        private readonly List<LineFollowerAlgorithm.Type> algorithms =
            new List<LineFollowerAlgorithm.Type>() { LineFollowerAlgorithm.Type.P, LineFollowerAlgorithm.Type.CUSTOM, LineFollowerAlgorithm.Type.PID };
        private readonly LineFollowerAlgorithmFactory algorithmFactory;

        #region Setters & Getters
        public List<LineFollowerAlgorithm.Type> Algorithms
        {
            get { return algorithms; }
        }

        public LineFollowerAlgorithm.Type CurrentAlgorithm
        {
            get { return currentAlgorithm; }
            set
            {
                currentAlgorithm = value;
                lineFollower.Algorithm = algorithmFactory.getAlgorithm(currentAlgorithm);
                NotifyPropertyChanged("CurrentAlgorithm");
            }
        }


        public LineFollowerController LineFollower
        {
            get
            {
                return lineFollower;
            }
            set
            {
                lineFollower = value;
                NotifyPropertyChanged("LineFollower");
            }
        }
        #endregion

        #region Actions

        public override ICommand Connect
        {
            get
            {
                if (null == connect)
                {
                    connect = new DelegateCommand(delegate
                    {
                        try
                        {
                            if (robot.Status == RemoteDevice.StatusE.CONNECTED) return;
                            Robot.connect();
                            driver = new LineFollowerDriver(robot, lineFollower);
                        }
                        catch (NotSupportedException)
                        {
                            // NONE (1st element in list) throws exception in order for this message to be handled
                            MessageBox.Show("Please choose valid robot and controller.", "Invalid settings");
                        }
                        catch
                        {
                            // Workaround. C# does not always manage its resources well when it comes to sockets.
                            robot.disconnect();
                        }
                    });
                }
                return connect;
            }
        }

        public override ICommand Disconnect
        {
            get
            {
                if (null == disconnect)
                {
                    disconnect = new DelegateCommand(delegate
                    {
                        if (robot != null && robot.Status == RemoteDevice.StatusE.CONNECTED)
                        {
                            robot.disconnect();
                        }
                    });
                }
                return disconnect;
            }
        }

        #endregion

        public LineFollowerViewModel()
        {
            lineFollower = new LineFollowerController {Sensors = new ObservableCollection<int>() {0, 0, 0, 0, 0}};
            algorithmFactory = new LineFollowerAlgorithmFactory();
            CurrentAlgorithm = LineFollowerAlgorithm.Type.P;
        }   
    }
}