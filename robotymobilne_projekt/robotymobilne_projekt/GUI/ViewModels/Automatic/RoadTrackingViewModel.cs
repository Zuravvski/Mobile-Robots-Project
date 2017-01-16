using robotymobilne_projekt.Devices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace robotymobilne_projekt.GUI.ViewModels.Automatic
{
    public class RoadTrackingViewModel : RobotViewModel
    {
        private string playPauseIcon;
        private bool isRunning;
        private bool isPainted;
        private readonly List<Point> points; //to store all coordinates

        private ICommand playPause;

        #region Constants

        public static readonly string PLAY_ICON = @"/Resources/Play.png";
        public static readonly string PAUSE_ICON = @"/Resources/Pause.png";

        #endregion

        #region Getters & Setters
        public string PlayPauseIcon
        {
            get
            {
                return playPauseIcon;
            }

            set
            {
                playPauseIcon = value;
                NotifyPropertyChanged("PlayPauseIcon");
            }
        }

        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                isRunning = value;
                NotifyPropertyChanged("IsRunning");
            }
        }

        public bool IsPainted
        {
            get
            {
                return isPainted;
            }
            set
            {
                isPainted = value;
                NotifyPropertyChanged("IsPainted");
            }
        }

        public List<Point> Points
        {
            get
            {
                return points;
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
                            if (robot.Status == RobotModel.StatusE.CONNECTED) return;
                            Robot.connect();
                        }
                        catch (NotSupportedException)
                        {
                            // NONE (1st element in list) throws exception in order for this message to be handled
                            MessageBox.Show("Please choose valid robot and controller.", "Invalid settings");
                        }
                        catch
                        {
                            // Workaround. C# does not always manage its resources well when it comes to sockets.
                            robot?.disconnect();
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
                        if (robot != null && robot.Status == RobotModel.StatusE.CONNECTED)
                        {
                            robot.disconnect();
                        }
                    });
                }
                return disconnect;
            }
        }

        public ICommand PlayPauseCommand
        {
            get
            {
                if (null == playPause)
                {
                    playPause = new DelegateCommand(delegate
                    {
                        if (isRunning)
                        {
                            IsRunning = false;
                            IsPainted = points.Count > 0;
                            PlayPauseIcon = PLAY_ICON;
                        }
                        else
                        {
                            IsRunning = true;
                            IsPainted = true;
                            PlayPauseIcon = PAUSE_ICON;
                        }
                    });
                }
                return playPause;
            }
        }
        #endregion

        public RoadTrackingViewModel()
        {
            PlayPauseIcon = PLAY_ICON;
            points = new List<Point>();
        }
    }
}
