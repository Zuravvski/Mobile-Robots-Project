namespace MobileRobots.Manual
{
    abstract class AbstractController
    {
        protected int latency;
        protected bool nitroPressed;
        protected bool handbrakePressed;
        protected RobotModel robot;

        /// <summary>
        /// Specifies the time controller is being polled for data.
        /// </summary>
        public int LATENCY
        {
            get
            {
                return latency;
            }
            set
            {
                latency = value;
            }
        }
        public RobotModel ROBOT
        {
            get
            {
                return robot;
            }
            set
            {
                robot = value;
            }
        }

        public AbstractController()
        {
            latency = 100; // default value of 20 ms
        }

        protected void CalculateFinalSpeed(double motorL, double motorR, double steerL, double steerR, bool nitro, bool handbrake)
        {
            if (null != robot)
            {
                motorL *= robot.MAXSPEED;
                motorR *= robot.MAXSPEED;

                steerL *= robot.STEERING_SENSIVITY;
                steerR *= robot.STEERING_SENSIVITY;

                if (handbrake)
                {
                    motorL = 0;
                    motorR = 0;
                }
                else
                {
                    if (nitro)
                    {
                        motorL *= robot.NITRO_VALUE;
                        motorR *= robot.NITRO_VALUE;
                    }

                    robot.SPEED_L = motorL + steerR;
                    robot.SPEED_R = motorR + steerL;
                }
            }
        }

        public string CalculateFrame(bool useLights, double speedL, double speedR)
        {
            //set get robot
            string frameLights = "00", frameL = "", frameR = "";

            if (useLights)
            {
                if (speedL == speedR)
                {
                    frameLights = "03";
                }
                if (speedR > speedL)
                {
                    frameLights = "02";
                }
                if (speedL > speedR)
                {
                    frameLights = "01";
                }
            }

            if (speedL >= 0)
            {
                frameL = ((int)speedL).ToString("X2");
            }
            else
            {
                frameL = ((int)speedL).ToString("X2").Substring(((int)speedL).ToString("X2").Length - 2);
            }

            if (speedR >= 0)
            {
                frameR = ((int)speedR).ToString("X2");
            }
            else
            {
                frameR = ((int)speedR).ToString("X2").Substring(((int)speedR).ToString("X2").Length - 2);
            }
            string finalFrame = frameLights + frameL + frameR;

            return finalFrame;
        }

        public double mapValues(double value, double fromSource, double toSource, double fromTarget, double toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }

        public abstract string execute();
    }
}
