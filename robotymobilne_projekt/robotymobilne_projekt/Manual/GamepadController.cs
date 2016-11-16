using System;
using OpenTK.Input;

namespace MobileRobots.Manual
{
    class GamepadController : AbstractController
    {
        private int index;
        private double RT, LT, RX, LX;
        private ButtonState A, B;

        public int GAMEPAD_INDEX
        {
            get
            {
                return index;
            }
        }

        public GamepadController(int index) : base()
        {
            this.index = index;
        }

        private void calculateSpeed()
        {
            double speed = 0, speedR = 0, speedL = 0;
            double steerR = 0, steerL = 0;
            double deadbound = 0.15;

            double RT = mapValues(this.RT, -1, 1, 0, 1);
            double LT = mapValues(this.LT, -1, 1, 0, 1);


            if (Math.Abs(RX) < deadbound)
            {
                speed = RT - LT;
                speedR = speed;
                speedL = speed;
            }

            if (Math.Abs(speed) < 0.1 && B == ButtonState.Released && Math.Abs(RX) > deadbound)   //turn
            {
                speedR = -RX;
                speedL = RX;
            }

            if (Math.Abs(speed) > 0.1 && Math.Abs(LX) > deadbound)  //steer
            {
                if (LX > 0)
                {
                    steerR = LX;
                }
                else
                {
                    steerL = Math.Abs(LX);
                }
            }

            if (A == ButtonState.Pressed)   //nitro
            {
                nitroPressed = true;
            }

            if (B == ButtonState.Pressed)   //brake
            {
                handbrakePressed = true;
            }

            CalculateFinalSpeed(speedL, speedR, steerL, steerR, nitroPressed, handbrakePressed);

            nitroPressed = false;
            handbrakePressed = false;
        }

        // XBox Input = Xinput
        private void getXinput()
        {
            //triggers
            RT = Joystick.GetState(index).GetAxis(JoystickAxis.Axis5);
            LT = Joystick.GetState(index).GetAxis(JoystickAxis.Axis2);

            //analogs
            RX = Joystick.GetState(index).GetAxis(JoystickAxis.Axis3);
            LX = Joystick.GetState(index).GetAxis(JoystickAxis.Axis0);

            //buttons
            A = Joystick.GetState(index).GetButton(JoystickButton.Button0);
            B = Joystick.GetState(index).GetButton(JoystickButton.Button1);
        }

        public override string execute()
        {
            getXinput();
            calculateSpeed();
            return CalculateFrame(robot.SPEED_L, robot.SPEED_R);
        }

        public override bool Equals(object obj)
        {
            // TODO: consider using typeof in case it doesn't work
            return index == ((GamepadController)obj).GAMEPAD_INDEX;
        }

        public override int GetHashCode()
        {
            return GAMEPAD_INDEX.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("Gamepad {0}", index+1);
        }
    }
}
