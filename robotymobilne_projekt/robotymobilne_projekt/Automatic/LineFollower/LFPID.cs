using System;
using System.Collections.ObjectModel;
using robotymobilne_projekt.Settings;

namespace robotymobilne_projekt.Automatic.LineFollower
{
    public class LFPID : LineFollowerAlgorithm
    {
        // Proportional part
        private double kp;

        // Derative part
        private double kd;
        private double derivative;

        // Integral part
        private double ki = 0;
        private double integral;

        // General part
        private double error;
        private double lastError;
        private const double Kc = 0.05;
        private const double dt = 0.02;
        private const double Pc = 0.38;

        #region Setters & Getters
        public double K_P
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
        public double K_D
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

        public double K_I
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

            var checksum = 0;
            foreach (var sensor in sensors)
            {
                checksum += sensor;
                if (checksum == 0 || checksum == sensors.Count*2000)
                {
                    return new Tuple<double, double>(0,0);
                }
            }

            kp = 0.5 * Kc;
            ki = 0.1 * (2 * kp * dt) / Pc;
            kd = 1 * (kp * Pc) / (8 * dt);

            error = readLine() - 2000;
            integral = 0.66 * integral + error;
            derivative = error - lastError;
            var turn = kp * error + ki * integral + kd * derivative;

            var motorL = RobotSettings.Instance.MaxSpeed / 2 + turn;
            var motorR = RobotSettings.Instance.MaxSpeed / 2 - turn;

            if (motorL < 0)
                motorL = Math.Abs(motorL);
            if (motorR < 0)
                motorR = Math.Abs(motorR);

            lastError = error;

            return new Tuple<double, double>(motorL, motorR);
        }
    }
}
