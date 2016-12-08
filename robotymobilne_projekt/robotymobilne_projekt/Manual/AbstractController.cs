using System;
using robotymobilne_projekt.Settings;
using robotymobilne_projekt.Utils;

namespace robotymobilne_projekt.Manual
{
    public abstract class AbstractController : ObservableObject
    {
        protected ControllerSettings controllerSettings;
        protected RobotSettings robotSettings;
        protected bool nitroPressed;
        protected bool handbrakePressed;
        protected double SpeedL;
        protected double SpeedR;
        protected bool nitro;
        protected bool isNotReserved;

        public bool IsNotReserved
        {
            get
            {
                return isNotReserved;
            }
            set
            {
                isNotReserved = value;
                NotifyPropertyChanged("IsNotReserved");
            }
        }

        /// <summary>
        /// Specifies the time controller is being polled for data.
        /// </summary>
        public AbstractController()
        {
            controllerSettings = ControllerSettings.Instance;
            robotSettings = RobotSettings.Instance;
            isNotReserved = true;
        }

        protected virtual void CalculateFinalSpeed(double motorL, double motorR, double steerL, double steerR, bool nitro, bool handbrake)
        {
            this.nitro = nitro;

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

        protected double mapValues(double value, double fromSource, double toSource, double fromTarget, double toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }

        public abstract Tuple<double, double> execute();
    }
}
