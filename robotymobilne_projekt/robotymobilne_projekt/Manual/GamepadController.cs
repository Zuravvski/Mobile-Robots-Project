using System;
using System.Windows.Threading;
using OpenTK.Input;
using robotymobilne_projekt.Utils;

namespace robotymobilne_projekt.Manual
{
    class GamepadController : IController
    {
        // Robot assigned to gamepad
        RobotModel robot;

        //user input:
        double nitroValue = 1.2;    //if more than 1.27 will be not fully useable on max speed
        double steerSensivity = 40; //sensivity of steering L-R
        double LRdeadbound = 0.05;  //deadbound on steering (dont set to more than 0.2)
        //
        private DispatcherTimer _timer;
        private float X, LT, RT;
        private ButtonState LB, RB;

        public GamepadController(RobotModel robot)
        {
            this.robot = robot;
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void getXinput(out float X, out float LT, out float RT, out ButtonState LB, out ButtonState RB)
        {
            var state = OpenTK.Input.Joystick.GetState(0);  //amount of gamepads, 0 - id
            if (true)   //FIX FOR MANY GAMEPADS,  state.isConnected
            {
                X = state.GetAxis(JoystickAxis.Axis0);
                LT = state.GetAxis(JoystickAxis.Axis2);
                RT = state.GetAxis(JoystickAxis.Axis5);
                LB = state.GetButton(JoystickButton.Button4);
                RB = state.GetButton(JoystickButton.Button5);
            }
        }

        private string calculateFrame(float X, float LT, float RT, ButtonState LB, ButtonState RB)
        {
            string frame;
            string frameL, frameR;
            double speedFL, speedFR;
            double speedBL, speedBR;
            double speedL, speedR;

            //speed forward(F) backward(B) for each side
            speedFR = ((RT + 1) * 50);
            speedFL = speedFR;

            speedBR = ((LT + 1) * 50);
            speedBL = speedBR;

            //speed for each side
            speedL = speedFL - speedBL;
            speedR = speedFR - speedBR;

            if (RB == ButtonState.Pressed)  //nitro
            {
                if (speedR > 0 && speedL > 0)   //case: forward
                {
                    speedL = speedL * nitroValue;
                    speedR = speedR * nitroValue;
                }
                else if (speedR < 0 && speedL < 0)  //case: backward
                {
                    speedL = speedL - (speedL * (nitroValue - 1));
                    speedR = speedR - (speedR * (nitroValue - 1));
                }
            }
            if (LB == ButtonState.Pressed)  //handbrake
            {
                speedL = 0;
                speedR = 0;
            }

            //left-right case: going forward
            if (X > 0 && X > LRdeadbound && speedL > 1 && speedR > 1)          //right
            {
                speedL = speedL + (steerSensivity * X);
            }
            else if (X < 0 && X < -LRdeadbound && speedL > 1 && speedR > 1)    //left
            {
                speedR = speedR - (steerSensivity * X);
            }


            //left-right case: going backward
            if (X > 0 && X > LRdeadbound && speedL < -1 && speedR < -1)          //left
            {
                speedL = speedL - (steerSensivity * X);
            }
            else if (X < 0 && X < -LRdeadbound && speedL < -1 && speedR < -1)    //right
            {
                speedR = speedR + (steerSensivity * X);
            }

            //to ensure that max speed isn't exceeded
            if (speedL > 127)
                speedL = 127;
            if (speedL < -127)
                speedL = -127;

            if (speedR > 127)
                speedR = 127;
            if (speedR < -127)
                speedR = -127;

            //convert double values to 2character hex 
            if (speedL >= 0)
                frameL = ((int)speedL).ToString("X2");
            else
                frameL = ((int)speedL).ToString("X2").Substring(((int)speedL).ToString("X2").Length - 2);

            if (speedR >= 0)
                frameR = ((int)speedR).ToString("X2");
            else
                frameR = ((int)speedR).ToString("X2").Substring(((int)speedR).ToString("X2").Length - 2);
            frame = "[03" + frameL + frameR + "]";

            return frame;
        }


        private void _timer_Tick(object sender, EventArgs e)
        {
            getXinput(out X, out LT, out RT, out LB, out RB);       //take input from gamepad
            string finalFrame = calculateFrame(X, LT, RT, LB, RB);  //calculate final frame
            execute(finalFrame);
        }

        public void execute(string action)
        {
            try
            {
                robot.sendDataFrame(action);
            }
            catch(Exception ex)
            {
                Logger.getLogger().log("The controller is not attached to any robot", ex);
            }
        }
    }
}
