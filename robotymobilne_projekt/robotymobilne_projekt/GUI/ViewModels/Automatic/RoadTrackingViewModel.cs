using robotymobilne_projekt.Devices;
using System;
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

        private ICommand playPause;

        #region Constants

        private const string PLAY_ICON = @"\Resources\Play.png";
        private const string PAUSE_ICON = @"\Resources\Pause.png";

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
        #endregion

        #region
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
                        if (robot != null && robot.Status == RemoteDevice.StatusE.CONNECTED)
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
                            isRunning = false;
                            PlayPauseIcon = PLAY_ICON;
                        }
                        else
                        {
                            isRunning = true;
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
        }
    }
}
