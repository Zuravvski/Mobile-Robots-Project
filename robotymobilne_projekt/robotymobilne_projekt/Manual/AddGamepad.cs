using System;
using OpenTK.Input;
using System.Collections.Generic;

namespace robotymobilne_projekt.Manual
{
    class AddGamepad
    {
        int joyAmount = 4;  //amount of gamepads
        private List<RobotModel> robots;


        public AddGamepad()
        {
            JoystickState[] joyState = new JoystickState[joyAmount];

            robots = new List<RobotModel>()
            {
                new RobotModel("30", "192.168.2.30", 8000),
                new RobotModel("31", "192.168.2.31", 8000),
                new RobotModel("32", "192.168.2.32", 8000),
                new RobotModel("33", "192.168.2.33", 8000),
                new RobotModel("34", "192.168.2.34", 8000),
            };


            for (int i = 0; i < 4; i++)
            {
                if (Joystick.GetState(i).IsConnected)
                {
                    new GamepadController(robots[0], 0);
                }
            }
            
        }
    }
}
