using System;
using System.Collections.ObjectModel;
using robotymobilne_projekt.Manual;
using robotymobilne_projekt.Settings;

namespace robotymobilne_projekt.Automatic
{
    public class LineFollower : AbstractController
    {
        // TODO: Tune controller
        // Proportional part
        private const int KP = 1; 

        // Derative part
        private const int KD = 1;

        // Integral part
        private int integral;

        // General part
        private int lastError;
        private int previousReading;

        public ObservableCollection<int> Sensors { private get; set; }


        public override Tuple<double, double> execute()
        {
            return secondVariant();
        }

        private Tuple<double, double> firstVariant()
        {
            var position = readLine();
            var error = position - 2000;
            var motorSpeed = KP * error + KD * (error - lastError);
            lastError = error;

            var motorL = 2 + motorSpeed;
            var motorR = 2 - motorSpeed;

            if (motorL < 0)
                motorL = 0;

            if (motorR < 0)
                motorR = 0;

            return new Tuple<double, double>(motorL, motorR);
        }

        private Tuple<double, double> secondVariant()
        {
            var motorL = RobotSettings.Instance.MaxSpeed;
            var motorR = RobotSettings.Instance.MaxSpeed;
            var position = readLine();

            // The "proportional" term should be 0 when we are on the line.
            var error = position - 2000;

            // Compute the derivative and integral of the
            // position.
            var derivative = error - lastError;
            integral += error;

            // Remember the last position.
            lastError = error;

            // Compute the difference between the two motor power settings,
            // m1 - m2.  If this is a positive number the robot will turn
            // to the right.  If it is a negative number, the robot will
            // turn to the left, and the magnitude of the number determines
            // the sharpness of the turn.
            var powerDifference = error / 20 + integral / 10000 + derivative * 3 / 2;

            // Compute the actual motor settings.  We never set either motor
            // to a negative value.
            if (powerDifference > RobotSettings.Instance.MaxSpeed)
                powerDifference = (int)RobotSettings.Instance.MaxSpeed;

            if (powerDifference < 0)
                powerDifference = 0;

            if (powerDifference < 0)
                motorR += powerDifference;
            else
                motorL += powerDifference;

            return new Tuple<double, double>(motorL, motorR);
        }

        private int readLine()
        {
            var isOnLine = false;
            long avg = 0; // this is for the weighted total, which is long before division
            var sum = 0; // this is for the denominator which is <= 64000

            callibrateSensors();

            for (var i = 0; i < Sensors.Count; i++)
            {
                var value = Sensors[i];

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
                if (previousReading < (Sensors.Count - 1) * 1000 / 2)
                    return 0;

                // If it last read to the right of center, return the max.
                else
                    return (Sensors.Count - 1) * 1000;
            }

            previousReading = (int) (avg / sum);

            return previousReading;
        }

        private void callibrateSensors()
        {
            for (var i = 0; i < Sensors.Count; i++)
            {
                Sensors[i] = (int) mapValues(Sensors[i], 0, 2000, 0, 1000);
            }
        }
    }
}
