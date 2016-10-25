using System;
using System.Windows.Threading;
using OpenTK.Input;
using robotymobilne_projekt.Utils;

namespace robotymobilne_projekt.Manual
{
    class GamepadController : IController
    {
        //user input:
        int maxspeed = 127;          //max speed using triggers only
        double nitrovalue = 1.8;    //if maxspeed * nitrovalue > 127 will be not fully useable 
        double steersensivity = 800; //sensivity of steering L-R
        double LRdeadbound = 0.1;  //deadbound on steering (dont set to more than 0.2)
        int turning = 100;          //sensivity of turning around
        int totalmaxspeed = 127;     //total max speed including nitro and RT  
        //
        // constructor variables:
        RobotModel robot;
        int padIndex;

        private DispatcherTimer _timer = new DispatcherTimer();
        private float XL, XR, LT, RT;
        private ButtonState B, A;


        public GamepadController(RobotModel robot)
        {
            this.robot = robot;
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        public GamepadController(RobotModel robot, int padIndex)
        {
            this.robot = robot;
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void getXinput(out float XL, out float XR, out float LT, out float RT, out ButtonState B, out ButtonState A)
        {
            var state = Joystick.GetState(padIndex);  //amount of gamepads, 0 - id
            if (true)   //FIX FOR MANY GAMEPADS,  state.isConnected
            {
                XL = state.GetAxis(JoystickAxis.Axis0);
                XR = state.GetAxis(JoystickAxis.Axis3);
                LT = state.GetAxis(JoystickAxis.Axis2);
                RT = state.GetAxis(JoystickAxis.Axis5);
                B = state.GetButton(JoystickButton.Button1);
                A = state.GetButton(JoystickButton.Button0);
            }
        }

        private string calculateFrame(float XL, float XR, float LT, float RT, ButtonState B, ButtonState A)
        {
            string frame;
            string frameL, frameR;
            int whichlights = 0; //1 - nitro, 2 - right, 3 - left
            string lights = "00";
            double speedFL, speedFR;
            double speedBL, speedBR;
            double speedL, speedR;

            //speed forward(F) backward(B) for each side
            speedFR = Map(RT, -1, 1, 0, maxspeed);
            speedFL = speedFR;

            speedBR = Map(LT, -1, 1, 0, maxspeed);
            speedBL = speedBR;

            //speed for each side
            speedL = speedFL - speedBL;
            speedR = speedFR - speedBR;

            if (XL < Math.Abs(LRdeadbound) && Math.Abs(speedR) < 1 && Math.Abs(speedL) < 1 && A == ButtonState.Released && B == ButtonState.Released)
            {
                if (XR > LRdeadbound)
                {
                    speedL = XR * turning;
                    speedR = -XR * turning;
                    lights = "01";
                }
                else if (XR < -LRdeadbound)
                {
                    speedL = XR * turning;
                    speedR = -XR * turning;
                    lights = "02";
                }
            }

            //left-right case: going forward
            if (XL > LRdeadbound && speedL > 1 && speedR > 1)          //right
            {
                speedL = speedL + (steersensivity * XL);
                whichlights = 2;
            }
            else if (XL < -LRdeadbound && speedL > 1 && speedR > 1)    //left
            {
                speedR = speedR - (steersensivity * XL);
                whichlights = 3;
            }


            //left-right case: going backward
            if (XL > 0 && XL > LRdeadbound && speedL < -1 && speedR < -1)          //left
            {
                speedL = speedL - (steersensivity * XL);
                whichlights = 3;
            }
            else if (XL < 0 && XL < -LRdeadbound && speedL < -1 && speedR < -1)    //right
            {
                speedR = speedR + (steersensivity * XL);
                whichlights = 2;
            }

            if (A == ButtonState.Pressed)  //nitro
            {
                if (speedR > 0 && speedL > 0)   //case: forward
                {
                    speedL = speedL * nitrovalue;
                    speedR = speedR * nitrovalue;
                }
                else if (speedR < 0 && speedL < 0)  //case: backward
                {
                    speedL = speedL - (speedL * (nitrovalue - 1));
                    speedR = speedR - (speedR * (nitrovalue - 1));
                }
                whichlights = 1;
            }

            if (B == ButtonState.Pressed)  //handbrake
            {
                speedL = 0;
                speedR = 0;
            }

            //to ensure that max speed isn't exceeded
            if (speedL > totalmaxspeed)
                speedL = totalmaxspeed;
            if (speedL < -totalmaxspeed)
                speedL = -totalmaxspeed;

            if (speedR > totalmaxspeed)
                speedR = totalmaxspeed;
            if (speedR < -totalmaxspeed)
                speedR = -totalmaxspeed;

            //convert double values to 2character hex 
            if (speedL >= 0)
                frameL = ((int)speedL).ToString("X2");
            else
                frameL = ((int)speedL).ToString("X2").Substring(((int)speedL).ToString("X2").Length - 2);

            if (speedR >= 0)
                frameR = ((int)speedR).ToString("X2");
            else
                frameR = ((int)speedR).ToString("X2").Substring(((int)speedR).ToString("X2").Length - 2);

            switch (whichlights)
            {
                case 1:
                    lights = "03";
                    break;
                case 2:
                    lights = "01";
                    break;
                case 3:
                    lights = "02";
                    break;
            }

            frame = "[" + lights + frameL + frameR + "]";

            return frame;
        }


        private void _timer_Tick(object sender, EventArgs e)
        {
            getXinput(out XL, out XR, out LT, out RT, out B, out A);   //take input from gamepad
            string finalFrame = calculateFrame(XL, XR, LT, RT, B, A);  //calculate final frame
            execute(finalFrame);

        }

        private float Map(float value, float fromSource, float toSource, float fromTarget, float toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
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
