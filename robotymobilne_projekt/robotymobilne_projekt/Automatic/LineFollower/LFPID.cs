using System;
using System.Collections.ObjectModel;
using robotymobilne_projekt.Settings;

namespace robotymobilne_projekt.Automatic.LineFollower
{
    public class LFPID : LineFollowerAlgorithm
    {
        // Proportional part
        private int kp = 1;

        // Derative part
        private int kd = 1;

        // Integral part
        private int ki = 1;
        private int integral;

        // General part
        private int lastError;

        #region Setters & Getters
        public int K_P
        {
            get
            {
                return kp;
            }
            set
            {
                kp = value;
                NotifyPropertyChanged("K_P");
            }
        }
        public int K_D
        {
            get
            {
                return kd;
            }
            set
            {
                kd = value;
                NotifyPropertyChanged("K_D");
            }
        }

        public int K_I
        {
            get
            {
                return ki;
            }
            set
            {
                ki = value;
                NotifyPropertyChanged("K_I");
            }
        }
        #endregion

        public override Tuple<double, double> execute(Collection<int> sensors)
        {
            this.sensors = sensors;

            var motorL = RobotSettings.Instance.MaxSpeed;
            var motorR = RobotSettings.Instance.MaxSpeed;
            var position = readLine();
            var error = 0;

            // The "proportional" term should be 0 when we are on the line.
            if (position != 0)
                error = position - 2000;

            // Compute the derivative and integral of the
            // position.
            var derivative = error - lastError;
            integral += error;

            // Remember the last position.
            lastError = error;

            var powerDifference = error / 20 + integral / 10000 + derivative * 3 / 2;

            if (powerDifference > RobotSettings.Instance.MaxSpeed)
                powerDifference = (int)RobotSettings.Instance.MaxSpeed;
            else
            {
                powerDifference = (int)RobotSettings.Instance.MaxSpeed * -1;
            }

            if (powerDifference < 0)
            {
                motorR += Math.Abs(powerDifference);
            }

            else
            {
                motorL += Math.Abs(powerDifference);
            }

            return new Tuple<double, double>(motorL, motorR);
        }
    }
}
