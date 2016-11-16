using System;
using OpenTK.Input;
using System.Windows.Threading;

namespace MobileRobots.Manual
{
    class KeyboardController : AbstractController
    {
        private KeyboardState key;

        public KeyboardController(KeyboardState key) : base()
        {
            this.key = key;
        }

        public override string execute()
        {
            getKinput();
            return CalculateFrame(false, robot.SPEED_L, robot.SPEED_R);
        }

        private void getKinput()
        {
            KeyboardState lol = Keyboard.GetState();
            key = lol;

            double speed = 0, speedR = 0, speedL = 0;
            double steerR = 0, steerL = 0;

            if (key.IsKeyDown(Key.Up))      //forward
            {
                speedR += 1;
                speedL += 1;
                speed += 1;
            }

            if (key.IsKeyDown(Key.Down))    //backward
            {
                speedR -= 1;
                speedL -= 1;
                speed -= 1;
            }

            if (key.IsKeyDown(Key.Right) && Math.Abs(speed) > 0)   //right
            {
                steerR = 1;
            }

            if (key.IsKeyDown(Key.Left) && Math.Abs(speed) > 0)    //left
            {
                steerL = 1;
            }

            if (key.IsKeyDown(Key.Period) && Math.Abs(speed) < 1 && speed < 1)
            {
                speedL = 1;
                speedR = -1;
                speed = 1;
            }

            if (key.IsKeyDown(Key.Comma) && Math.Abs(speed) < 1 && speed < 1)
            {
                speedR = 1;
                speedL = -1;
                speed = 1;
            }

            if (key.IsKeyDown(Key.LShift))  //nitro
            {
                nitroPressed = true;
            }

            if (key.IsKeyDown(Key.Space))   //brake
            {
                handbrakePressed = true;
            }

            CalculateFinalSpeed(speedL, speedR, steerL, steerR, nitroPressed, handbrakePressed);
        }
    }
}
