using MobileRobots;
using MobileRobots.Manual;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotymobilne_projekt.Manual
{
    class Hub
    {
        private List<RobotModel> robots;
        private List<AbstractController> controllers;
        

        public Hub()
        {
            scanPads();
            // Hardode 2 default keyboard controllers
        }

        // Implement more sophisticated logic
        public void scanPads()
        {
            for(int i = 0; i < 4; i++)
            {
                if(Joystick.GetState(i).IsConnected)
                {
                    controllers.Add(new GamepadController(i));
                }
            }
        }
    }
}
