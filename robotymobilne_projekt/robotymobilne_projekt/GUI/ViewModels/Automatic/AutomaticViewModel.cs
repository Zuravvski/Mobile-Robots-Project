using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using robotymobilne_projekt.GUI.Views.Automatic;

namespace robotymobilne_projekt.GUI.ViewModels.Automatic
{
    public class AutomaticViewModel : ViewModel
    {
        private readonly List<AutomaticMode> modes = new List<AutomaticMode>(){AutomaticMode.LINE_FOLLOWER, AutomaticMode.ROAD_TRACKING };
        private readonly AutomaticPanelFactory panelFactory;
        private AutomaticMode currentMode;
        private readonly AutomaticView context;

        #region Setters & Getters
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
                context.viewboxModeArea.Child = panelFactory.getPanel(currentMode);
                NotifyPropertyChanged("CurrentMode");
            }
        }

        public List<AutomaticMode> Modes
        {
            get { return modes; }
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
