namespace robotymobilne_projekt.Automatic.LineFollower
{
    public class LineFollowerAlgorithmFactory
    {
        public LineFollowerAlgorithm getAlgorithm(LineFollowerAlgorithm.Type type)
        {
            switch (type)
            {
                case LineFollowerAlgorithm.Type.P:
                case LineFollowerAlgorithm.Type.PID:
                default:
                    return new LFPID();   
            }
        }
    }
}
