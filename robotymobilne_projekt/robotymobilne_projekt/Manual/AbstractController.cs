using robotymobilne_projekt.Settings;

namespace MobileRobots.Manual
{
    public abstract class AbstractController
    {
        protected ControllerSettings controllerSettings;
        protected RobotSettings robotSettings;
        protected bool nitroPressed;
        protected bool handbrakePressed;
        protected double SpeedL;
        protected double SpeedR;


        #region Constants
        private const string noLights = "00";
        private const string leftLight = "01";
        private const string rightLight = "02";
        private const string bothLights = "03";
        #endregion


        /// <summary>
        /// Specifies the time controller is being polled for data.
        /// </summary>
        public AbstractController()
        {
            controllerSettings = ControllerSettings.Instance;
            robotSettings = RobotSettings.Instance;
        }

        protected virtual void CalculateFinalSpeed(double motorL, double motorR, double steerL, double steerR, bool nitro, bool handbrake)
        {
            motorL *= robotSettings.MaxSpeed;
            motorR *= robotSettings.MaxSpeed;

            steerL *= robotSettings.SteeringSensivity;
            steerR *= robotSettings.SteeringSensivity;

            if (motorL > 0 && motorR > 0)
            {
                if (nitro)
                {
                    motorL *= robotSettings.NitroFactor;
                    motorR *= robotSettings.NitroFactor;
                }
                SpeedL = motorL + steerR;
                SpeedR = motorR + steerL;
            }
            else
            {
                SpeedL = motorL - steerR;
                SpeedR = motorR - steerL;
            }

            if (handbrake)
            {
                SpeedL = 0;
                SpeedR = 0;
            }
        }

        protected virtual string CalculateFrame(double speedL, double speedR)
        {
            //set get robot
            string frameLights = noLights, frameL = string.Empty, frameR = string.Empty;

            if (robotSettings.UseLights)
            {
                if (speedL == speedR)
                {
                    frameLights = bothLights;
                }
                if (speedR > speedL)
                {
                    frameLights = rightLight;
                }
                if (speedL > speedR)
                {
                    frameLights = leftLight;
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

        protected double mapValues(double value, double fromSource, double toSource, double fromTarget, double toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }

        public abstract string execute();
    }
}
