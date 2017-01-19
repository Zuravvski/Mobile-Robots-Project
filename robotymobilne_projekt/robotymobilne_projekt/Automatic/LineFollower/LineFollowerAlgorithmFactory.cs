namespace robotymobilne_projekt.Automatic.LineFollower
{
    public class LineFollowerAlgorithmFactory
    {
        public LineFollowerAlgorithm getAlgorithm(LineFollowerAlgorithm.Type type)
        {
            switch (type)
            {
                default:
                case LineFollowerAlgorithm.Type.PID:
                    return new PIDAlgorithm();
            }
        }
    }
}
