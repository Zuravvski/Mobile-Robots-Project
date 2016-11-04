namespace MobileRobots.Manual
{
    public abstract class AbstractController
    {
        protected int latency;
        protected double speedL, speedR;
        protected bool nitro;
        protected bool handbrake;

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

        public AbstractController()
        {
            latency = 100; // default value of 20 ms
        }

        protected void CalculateFinalSpeed(double speedL, double speedR, double steerL, double steerR, bool nitro, bool handbrake,
            double maxSpeed, double steeringSensivity, double nitroValue)
        {
            speedL *= maxSpeed;
            speedR *= maxSpeed;

            steerL *= steeringSensivity;
            steerR *= steeringSensivity;

            if (nitro)
            {
                speedL *= nitroValue;
                speedR *= nitroValue;
            }

            if (handbrake)
            {
                speedL = 0;
                speedR = 0;
            }

            this.speedL = speedL + steerR;
            this.speedR = speedR + steerL;

            if (this.speedR > 127)
            {
                this.speedR = 127;
            }

            if (this.speedL > 127)
            {
                this.speedL = 127;
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
