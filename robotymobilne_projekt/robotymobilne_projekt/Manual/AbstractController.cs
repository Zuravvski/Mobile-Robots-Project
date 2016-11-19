using robotymobilne_projekt.Settings;

namespace MobileRobots.Manual
{
    abstract class AbstractController
    {
        protected ControllerSettings controllerSettings;
        protected RobotSettings robotSettings;
        protected RobotModel robot;
        protected bool nitroPressed;
        protected bool handbrakePressed;

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

        /// <summary>
        /// Specifies the time controller is being polled for data.
        /// </summary>
        public AbstractController()
        {
            controllerSettings = ControllerSettings.INSTANCE;
            robotSettings = RobotSettings.INSTANCE;
        }

        protected void CalculateFinalSpeed(double motorL, double motorR, double steerL, double steerR, bool nitro, bool handbrake)
        {
            motorL *= robotSettings.MAX_SPEED;
            motorR *= robotSettings.MAX_SPEED;

            steerL *= robotSettings.STEERING_SENSIVITY;
            steerR *= robotSettings.STEERING_SENSIVITY;

            if (nitro)
            {
                motorL *= robotSettings.NITRO_VALUE;
                motorR *= robotSettings.NITRO_VALUE;
            }

            robot.SPEED_L = motorL + steerR;
            robot.SPEED_R = motorR + steerL;

            if (handbrake)
            {
                robot.SPEED_L = 0;
                robot.SPEED_R = 0;
            }

        }

        public string CalculateFrame(double speedL, double speedR)
        {
            //set get robot
            string frameLights = "00", frameL = "", frameR = "";

            if (robotSettings.USE_LIGHTS)
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
