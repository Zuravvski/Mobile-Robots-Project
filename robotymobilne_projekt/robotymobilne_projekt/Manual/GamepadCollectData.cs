using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace robotymobilne_projekt.Manual
{
    class GamepadCollectData
    {
        private float XL, XR, LT, RT;
        private ButtonState A, B;

        public GamepadCollectData(JoystickState state)
        {
            {
                XL = state.GetAxis(JoystickAxis.Axis0);
                XR = state.GetAxis(JoystickAxis.Axis3);
                LT = state.GetAxis(JoystickAxis.Axis2);
                RT = state.GetAxis(JoystickAxis.Axis5);
                B = state.GetButton(JoystickButton.Button1);
                A = state.GetButton(JoystickButton.Button0);
            }
        }
    }
}
