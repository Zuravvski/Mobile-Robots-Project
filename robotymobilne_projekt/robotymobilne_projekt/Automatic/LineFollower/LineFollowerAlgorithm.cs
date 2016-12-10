using System;
using System.Collections.ObjectModel;
using robotymobilne_projekt.Utils;

namespace robotymobilne_projekt.Automatic.LineFollower
{
    public abstract class LineFollowerAlgorithm : ObservableObject
    {
        public abstract Tuple<double, double> execute(Collection<int> sensors);
        public enum Type { P, PID }
    } 
}
