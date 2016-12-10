using System;
using System.Collections.ObjectModel;
using robotymobilne_projekt.Manual;
using robotymobilne_projekt.Settings;

namespace robotymobilne_projekt.Automatic.LineFollower
{
    public class LFPID : LineFollowerAlgorithm
    {
        private Collection<int> sensors;

        // Proportional part
        private int KP = 1;

        // Derative part
        private int KD = 1;

        // Integral part
        private int KI = 1;
        private int integral;

        // General part
        private int lastError;
        private int previousReading;

        public int K_P
        {
            get
            {
                return KP;
            }
            set
            {
                KP = value;
                NotifyPropertyChanged("K_P");
            }
        }
        public int K_D
        {
            get
            {
                return KD;
            }
            set
            {
                KD = value;
                NotifyPropertyChanged("K_D");
            }
        }

        public int K_I
        {
            get
            {
                return KI;
            }
            set
            {
                KI = value;
                NotifyPropertyChanged("K_I");
            }
        }

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

        private int readLine()
        {
            var isOnLine = false;
            long avg = 0; // this is for the weighted total, which is long before division
            var sum = 0; // this is for the denominator which is <= 64000

            callibrateSensors();

            for (var i = 0; i < sensors.Count; i++)
            {
                var value = sensors[i];

                // keep track of whether we see the line at all
                if (value > 200)
                {
                    isOnLine = true;
                }

                // only average in values that are above a noise threshold
                if (value > 50)
                {
                    avg += (long)(value) * (i * 1000);
                    sum += value;
                }
            }

            if (!isOnLine)
            {
                // If it last read to the left of center, return 0.
                if (previousReading < (sensors.Count - 1) * 1000 / 2)
                    return 0;

                // If it last read to the right of center, return the max.
                else
                    return (sensors.Count - 1) * 1000;
            }

            previousReading = (int)(avg / sum);

            return previousReading;
        }

        private void callibrateSensors()
        {
            for (var i = 0; i < sensors.Count; i++)
            {
                sensors[i] = (int)AbstractController.mapValues(sensors[i], 0, 2000, 0, 1000);
            }
        }
    }
}
