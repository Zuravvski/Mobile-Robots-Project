using System.Windows.Controls;
using robotymobilne_projekt.GUI.Views.Automatic;

namespace robotymobilne_projekt.GUI.ViewModels.Automatic
{
    public class AutomaticViewModel : ViewModel
    {
        private readonly AutomaticMode[] modes = new []{AutomaticMode.INVALID, AutomaticMode.LINE_FOLLOWER, AutomaticMode.TRAJECTORY};
        private readonly AutomaticPanelFactory panelFactory;
        private AutomaticMode currentMode;

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
                NotifyPropertyChanged("CurrentMode");
                CurrentPanel = panelFactory.getPanel(currentMode);
            }
        }

        public AutomaticMode[] Modes
        {
            get { return modes; }
        }
        #endregion

        public AutomaticViewModel()
        {
            panelFactory = new AutomaticPanelFactory();
        }
    }
}
