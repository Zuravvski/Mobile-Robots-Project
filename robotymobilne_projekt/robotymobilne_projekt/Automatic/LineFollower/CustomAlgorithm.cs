using System;
using System.Collections.ObjectModel;
using robotymobilne_projekt.Settings;

namespace robotymobilne_projekt.Automatic.LineFollower
{
    public class CustomAlgorithm : LineFollowerAlgorithm
    {
        private double error = 0;
        private const double blackV = 1100;
        private const double whiteV = 300;

        private double Kc = 0.05;
        private double dt = 0.02;
        private double Pc = 0.38;

        private double Kp = 0;
        private double Ki = 0;
        private double Kd = 0;
        private double integral = 0;
        private double derivative = 0;
        private double lastErr = 0;

        private double turn = 0;

        public ObservableCollection<int> Sensors { set; get; }

        public override Tuple<double, double> execute(Collection<int> sensors)
        {
            double motorL = 0;
            double motorR = 0;

            Kp = 0.5 * Kc;
            Ki = 0.1 * (2 * Kp * dt) / Pc;
            Kd = 1 * (Kp * Pc) / (8 * dt);
            //Ki = 0;
            //Kd = 0;

            var offset = (blackV + whiteV) / 2;


            error = (double)Sensors[2] - offset;
            integral = 0.66 * integral + error;
            derivative = error - lastErr;
            turn = Kp * error + Ki * integral + Kd * derivative;

            motorL = RobotSettings.Instance.MaxSpeed / 2 + turn;
            motorR = RobotSettings.Instance.MaxSpeed / 2 - turn;

            if (motorL < 0)
                motorL = motorL * -1;
            if (motorR < 0)
                motorR = motorR * -1;

            lastErr = error;
            return new Tuple<double, double>(motorL, motorR);
        }
    }
}
