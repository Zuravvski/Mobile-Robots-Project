using System;
using System.Collections.ObjectModel;
using robotymobilne_projekt.Settings;

namespace robotymobilne_projekt.Automatic.LineFollower
{
    public class CustomAlgorithm : LineFollowerAlgorithm
    {
        //MOJE
        double error = 0;
        double blackV = 0;
        double whiteV = 2000;

        //double Kp = (0 - RobotSettings.Instance.MaxSpeed)/(-offset - 0);
        double Kc = 0.05;
        double dt = 0.02;
        double Pc = 0.38;
        int i = 0;



        double Kp = 0;
        double Ki = 0;
        double Kd = 0;
        double integral = 0;
        double derivative = 0;
        double lastErr = 0;

        double turn = 0;

        public ObservableCollection<int> Sensors { set; get; }

        public override Tuple<double, double> execute(Collection<int> sensors)
        {

            if (Sensors[2] < whiteV)
                whiteV = Sensors[2];
            if (Sensors[2] > blackV)
                blackV = Sensors[2];

            double motorL = 0;
            double motorR = 0;

            Kp = 0.17 * Kc;
            Kp = 0.3 * Kc;
            Ki = 0.5 * (2 * Kp * dt) / Pc;
            Kd = 4 * (Kp * Pc) / (8 * dt);



            double offset = (blackV + whiteV) / 2;


            error = (double)Sensors[2] - offset;
            integral = (2 / 3) * integral + error;
            derivative = error - lastErr;
            turn = Kp * error + Ki * integral + Kd * derivative;

            motorL = RobotSettings.Instance.MaxSpeed / 2 + turn;
            motorR = RobotSettings.Instance.MaxSpeed / 2 - turn;

            if (Math.Abs(motorL) > RobotSettings.Instance.MaxSpeed)
                motorL = RobotSettings.Instance.MaxSpeed;
            if (Math.Abs(motorR) > RobotSettings.Instance.MaxSpeed)
                motorR = RobotSettings.Instance.MaxSpeed;

            //if (motorL < 0)
            //    motorL = motorL * -1;
            //if (motorR < 0)
            //    motorR = motorR * -1;

            lastErr = error;
            return new Tuple<double, double>(motorL, motorR);
        }
    }
}
