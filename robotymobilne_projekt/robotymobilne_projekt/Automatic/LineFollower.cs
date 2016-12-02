using System;
using robotymobilne_projekt.Manual;

namespace robotymobilne_projekt.Automatic
{
    public class LineFollower : AbstractController
    {
        // TODO: Tune controller
        // Proportional part
        private const int KP = 1; 

        // Derative part
        private const int KD = 1;

        // General part
        private int lastError;
        private int previousReading;

        public int[] Sensors { private get; set; }


        public override Tuple<double, double> execute()
        {
            var position = readLine();
            var error = position - 1000;
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

        private int readLine()
        {
            var isOnLine = false;
            long avg = 0; // this is for the weighted total, which is long before division
            var sum = 0; // this is for the denominator which is <= 64000

            callibrateSensors();

            for (var i = 0; i < Sensors.Length; i++)
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
                if (previousReading < (Sensors.Length - 1) * 1000 / 2)
                    return 0;

                // If it last read to the right of center, return the max.
                else
                    return (Sensors.Length - 1) * 1000;
            }

            previousReading = (int) (avg / sum);

            return previousReading;
        }

        private void callibrateSensors()
        {
            for (var i = 0; i < Sensors.Length; i++)
            {
                Sensors[i] = (int) mapValues(Sensors[i], 0, 2048, 0, 1000);
            }
        }
    }
}
