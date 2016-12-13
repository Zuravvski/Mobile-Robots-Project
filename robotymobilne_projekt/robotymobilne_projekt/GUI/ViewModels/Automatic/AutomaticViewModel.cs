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
        private ObservableCollection<UserControl> currentPanel;
        private RobotViewModel currentPanelDataContext;
        private AutomaticMode currentMode;
        private RobotModel currentRobot;

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
                if (null != currentPanelDataContext)
                {
                    currentPanelDataContext.Robot = currentRobot;
                }
                NotifyPropertyChanged("CurrentRobot");
            }
        }
        public ObservableCollection<RobotModel> Robots
        {
            get { return RobotSettings.Instance.Robots; }
        }

        public ObservableCollection<UserControl> CurrentPanel
        {
            get
            {
                return currentPanel;
            }
        }

        public AutomaticMode CurrentMode
        {
            get
            {
                return currentMode;
            }
            set
            {
                currentMode = value;
                CurrentPanel[0] = panelFactory.getPanel(currentMode);
                if (null != currentPanel[0])
                {
                    currentPanelDataContext = (RobotViewModel)currentPanel[0].DataContext;
                }
                else
                {
                    currentPanelDataContext = null;
                }
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
                        currentPanelDataContext?.Connect.Execute(null);
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
                        currentPanelDataContext?.Disconnect.Execute(null);
                    }); 
                }
                return disconnect;
            }
        }
        #endregion

        public AutomaticViewModel()
        {
            panelFactory = new AutomaticPanelFactory();
            currentMode = AutomaticMode.NONE;
            currentPanel = new ObservableCollection<UserControl>() {null};
        }
    }
}
