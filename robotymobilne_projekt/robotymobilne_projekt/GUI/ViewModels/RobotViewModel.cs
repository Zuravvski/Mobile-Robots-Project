using MobileRobots;
using MobileRobots.Manual;
using robotymobilne_projekt.Settings;
using System.Collections.Generic;

namespace robotymobilne_projekt.GUI.ViewModels
{
    class RobotViewModel
    {
        public List<RobotModel> ROBOTS
        {
            get
            {
                return RobotSettings.INSTANCE.AVAILABLE_ROBOTS;
            }
        }
        public List<AbstractController> CONTROLLERS
        {
            get
            {
                return ControllerSettings.INSTANCE.AVAILABLE_CONTROLLERS;
            }
        }
        public RobotModel ROBOT { set; get; }

        public RobotViewModel()
        {
            
        }
    }
}
