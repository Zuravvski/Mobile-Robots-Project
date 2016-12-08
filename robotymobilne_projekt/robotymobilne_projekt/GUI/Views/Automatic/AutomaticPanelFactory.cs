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

                case AutomaticMode.TRAJECTORY:
                    //return new TrajectoryView();
                case AutomaticMode.INVALID:
                    panel = null;
                    break;
            }
            return panel;
        }
    }

    public enum AutomaticMode
    {
        INVALID,
        LINE_FOLLOWER,
        TRAJECTORY
    };
}
