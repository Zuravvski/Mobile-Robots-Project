using System.Windows.Controls;

namespace robotymobilne_projekt.GUI.Views.Automatic
{
    public class AutomaticPanelFactory
    {
        public UserControl getPanel(AutomaticMode type)
        {
            UserControl panel = null;
            switch (type)
            {
                case AutomaticMode.LINE_FOLLOWER:
                    panel = new LineFollowerView();
                    break;

                case AutomaticMode.ROAD_TRACKING:
                    panel = new RoadTrackingView();
                    break;
            }
            return panel;
        }
    }

    public enum AutomaticMode
    {
        NONE,
        LINE_FOLLOWER,
        ROAD_TRACKING
    };
}
