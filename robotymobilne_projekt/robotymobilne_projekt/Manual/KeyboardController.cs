using System;
using OpenTK.Input;

namespace MobileRobots.Manual
{
    public class KeyboardController : AbstractController
    {
        private int index;
        private KeyboardState key;
        private Key forward;
        private Key backward;
        private Key left;
        private Key right;
        private readonly Key leftTurn;
        private readonly Key rightTurn;
        private readonly Key nitro;
        private readonly Key handbrake;

        public KeyboardController(int index, KeyboardState key, Key forward, Key backward, Key left, Key right,
                Key leftTurn, Key rightTurn, Key nitro, Key handbrake) : base()
        {
            this.index = index;
            this.key = key;
            this.forward = forward;
            this.backward = backward;
            this.left = left;
            this.right = right;
            this.leftTurn = leftTurn;
            this.rightTurn = rightTurn;
            this.nitro = nitro;
            this.handbrake = handbrake;
        }

        public override string execute()
        {
            key = Keyboard.GetState();
            getKinput();
            return CalculateFrame(SpeedL, SpeedR);
        }

        private void getKinput()
        { 
            double speed = 0, speedR = 0, speedL = 0;
            double steerR = 0, steerL = 0;

            if (key.IsKeyDown(forward))      //forward
            {
                speedR += 1;
                speedL += 1;
                speed += 1;
            }

            if (key.IsKeyDown(backward))    //backward
            {
                speedR -= 1;
                speedL -= 1;
                speed -= 1;
            }

            if (key.IsKeyDown(right) && Math.Abs(speed) > 0)   //right
            {
                steerR = 0.6;
            }

            if (key.IsKeyDown(left) && Math.Abs(speed) > 0)    //left
            {
                steerL = 0.6;
            }

            if (key.IsKeyDown(leftTurn) && Math.Abs(speed) < 1 && speed < 1)
            {
                speedL = 1;
                speedR = -1;
                speed = 1;
            }

            if (key.IsKeyDown(rightTurn) && Math.Abs(speed) < 1 && speed < 1)
            {
                speedR = 1;
                speedL = -1;
                speed = 1;
            }

            if (key.IsKeyDown(nitro))  //nitro
            {
                nitroPressed = true;
            }

            if (key.IsKeyDown(handbrake))   //brake
            {
                handbrakePressed = true;
            }

            CalculateFinalSpeed(speedL, speedR, steerL, steerR, nitroPressed, handbrakePressed);

            nitroPressed = false;
            handbrakePressed = false;
        }

        public override string ToString()
        {
            return string.Format("Keyboard {0}", index+1);
        }
    }
}
