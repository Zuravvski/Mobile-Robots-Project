using System;
using System.Collections.ObjectModel;
using robotymobilne_projekt.Utils;

namespace robotymobilne_projekt.Automatic.LineFollower
{
    public abstract class LineFollowerAlgorithm : ObservableObject
    {
        public enum Type { P, PID } // Algorithm type
        protected Collection<int> sensors;
        private int previousReading;

        public abstract Tuple<double, double> execute(Collection<int> sensors);
        
        protected int readLine()
        {
            var isOnLine = false;
            long avg = 0;
            var sum = 0;

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
    } 
}
