using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using robotymobilne_projekt.Devices;
using robotymobilne_projekt.GUI.Views.Automatic;
using robotymobilne_projekt.Settings;

namespace robotymobilne_projekt.GUI.ViewModels.Automatic
{
    public class AutomaticViewModel : ViewModel
    {
        // Commands
        private ICommand connect;
        private ICommand disconnect;

        // Fields
        private readonly List<AutomaticMode> modes = new List<AutomaticMode>(){AutomaticMode.LINE_FOLLOWER, AutomaticMode.ROAD_TRACKING };
        private readonly AutomaticPanelFactory panelFactory;
        private AutomaticMode currentMode;
        private readonly AutomaticView context;
        private RobotModel currentRobot;
        private RobotDriver driver;

        #region Setters & Getters

        public RobotModel CurrentRobot
        {
            get
            {
                return currentRobot;
            }
            set
            {
                currentRobot = value;
                if (CurrentPanel != null && CurrentPanel is LineFollowerView)
                {
                    // TODO : Provide common Abstraction
                    ((LineFollowerViewModel) CurrentPanel.DataContext).Robot = currentRobot;      
                }
                NotifyPropertyChanged("CurrentRobot");
            }
        }
        public ObservableCollection<RobotModel> Robots
        {
            get { return RobotSettings.Instance.Robots; }
        }
        public UserControl CurrentPanel { get; set; }

        public AutomaticMode CurrentMode
        {
            get
            {
                return currentMode;
            }
            set
            {
                currentMode = value;
                CurrentPanel = panelFactory.getPanel(currentMode);
                context.viewboxModeArea.Child = CurrentPanel;
                NotifyPropertyChanged("CurrentMode");
            }
        }

        public List<AutomaticMode> Modes
        {
            get { return modes; }
        }
        #endregion

        #region Actions
        public ICommand Connect
        {
            get
            {
                if (null == connect)
                {
                    connect = new DelegateCommand(delegate
                    {
                        try
                        {
                            if (currentRobot.Status == RemoteDevice.StatusE.CONNECTED) return;

                            CurrentRobot.connect();
                        }
                        catch (NotSupportedException)
                        {
                            // NONE (1st element in list) throws exception in order for this message to be handled
                            MessageBox.Show("Please choose valid robot and controller.", "Invalid settings");
                        }
                        catch
                        {
                            // Workaround. C# does not always manage its resources well when it comes to sockets.
                            currentRobot.disconnect();
                        }
                    });
                }
                return connect;
            }
        }

        public ICommand Disconnect
        {
            get
            {
                if (null == disconnect)
                {
                    disconnect = new DelegateCommand(delegate
                    {
                        if (CurrentRobot != null && CurrentRobot.Status == RemoteDevice.StatusE.CONNECTED)
                        {
                            CurrentRobot.disconnect();
                        }
                    });
                }
                return disconnect;
            }
        }
        #endregion

        public AutomaticViewModel(AutomaticView context)
        {
            panelFactory = new AutomaticPanelFactory();
            currentMode = AutomaticMode.NONE;
            this.context = context;
        }
    }
}
