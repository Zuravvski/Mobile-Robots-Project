using robotymobilne_projekt.Automatic;
using robotymobilne_projekt.Devices;
using robotymobilne_projekt.GUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotymobilne_projekt.GUI.Views.Automatic
{
    public class LineFollowerViewModel : ViewModel
    {
        public RobotModel Robot { get; set; }
        public LineFollower LineF { get; set; }

        public LineFollowerViewModel(RobotModel robot, LineFollower linef)
        {
            Robot = robot;
            LineF = linef;
        }
    }
}
