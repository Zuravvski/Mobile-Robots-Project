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
        private int KP = 1; 

        // Derative part
        private int KD = 1;

        // Integral part
        //private int integral;

        // General part
        private int lastError;
        private int previousReading;

        //MOJE
        double error = 0;
        double blackV = 1100;
        double whiteV = 300;

        //double Kp = (0 - RobotSettings.Instance.MaxSpeed)/(-offset - 0);
        double Kc = 0.05;
        double dt = 0.02;
        double Pc = 0.38;



        double Kp = 0;
        double Ki = 0;
        double Kd = 0;
        double integral = 0;
        double derivative = 0;
        double lastErr = 0;

        double turn = 0;

        


        private ObservableCollection<int> sensors;

        #region Setters & Getters
        public ObservableCollection<int> Sensors
        {
            private get
            {
                return sensors;
            }
            set
            {
                sensors = value;
                NotifyPropertyChanged("Sensors");
            }
        }

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

        public int PreviousError
        {
            get { return lastError; }
            set
            {
                lastError = value;
                NotifyPropertyChanged("PreviousError");
            }
        }

        #endregion

        public override Tuple<double, double> execute() 
        {
            return sixthVariant();
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

        private Tuple<double, double> thirdVariant()
        {
            double motorL = 0;
            double motorR = 0;
            double deadbond = 100;
            double x = 0;
            
            if (Sensors[2] > Sensors[0] && Sensors[2] > Sensors[1] && Sensors[2] > Sensors[3] && Sensors[2] > Sensors[4])
            {
                motorL = RobotSettings.Instance.MaxSpeed;
                motorR = RobotSettings.Instance.MaxSpeed;
            }
            else if (Sensors[3] > Sensors[1] && Sensors[2] < Sensors[3])    //rotate RIGHT
            {
                //x = mapValues(Sensors[3] - Sensors[2], 0, 700, 0, 0.2);
                x = (Sensors[3] - Sensors[2]) / Sensors[3];
                motorL = RobotSettings.Instance.MaxSpeed + x * RobotSettings.Instance.MaxSpeed;
                motorR = 0.5 * RobotSettings.Instance.MaxSpeed - x * RobotSettings.Instance.MaxSpeed;
            }
            else if (Sensors[1] > Sensors[3] && Sensors[1] > Sensors[2])    //rotate LEFT
            {
                //x = mapValues(Sensors[1] - Sensors[2], 0, 700, 0, 0.2);
                x = 0.3*(double)(Sensors[1] - Sensors[2]) / (double)Sensors[1];
                motorL = RobotSettings.Instance.MaxSpeed - x * RobotSettings.Instance.MaxSpeed;
                motorR = RobotSettings.Instance.MaxSpeed + x * RobotSettings.Instance.MaxSpeed;
            }
            else if (Sensors[0] > Sensors[2] && Sensors[0] > Sensors[1] && Math.Abs(Sensors[0] - Sensors[2]) > deadbond)    //rotate LEFT HARD
            {
                //x = mapValues(Sensors[0] - Sensors[2], 0, 1300, 0, 0.7);
                x = (Sensors[0] - Sensors[2]) / Sensors[0];
                motorL = RobotSettings.Instance.MaxSpeed - x * RobotSettings.Instance.MaxSpeed;
                motorR = RobotSettings.Instance.MaxSpeed + x * RobotSettings.Instance.MaxSpeed;
            }
            else if (Sensors[4] > Sensors[2] && Sensors[4] > Sensors[3] && Math.Abs(Sensors[4] - Sensors[2]) > deadbond)    //rotate RIGHT HARD
            {
                //x = mapValues(Sensors[3] - Sensors[2], 0, 1300, 0, 0.7);
                x = 0.5*(double)(Sensors[3] - Sensors[2]) / (double)Sensors[3];
                motorL = RobotSettings.Instance.MaxSpeed + x * RobotSettings.Instance.MaxSpeed;
                motorR = 0.5 * RobotSettings.Instance.MaxSpeed - x * RobotSettings.Instance.MaxSpeed;
            }
            //return new Tuple<double, double>(motorL, motorR);
            return new Tuple<double, double>(0, 0);
        }


        private Tuple<double, double> fourthVariant()   //P
        {
            double motorL = 0;
            double motorR = 0;

            double offset = (blackV + whiteV) / 2;
            Kp = 0.6 * Kc;

            error = (double)Sensors[2] - offset;
            turn = Kp * error;

            motorL = RobotSettings.Instance.MaxSpeed/2 + turn;
            motorR = RobotSettings.Instance.MaxSpeed/2 - turn;

            return new Tuple<double, double>(motorL, motorR);
        }

        private Tuple<double, double> fifthVariant()    //PI
        {
            double motorL = 0;
            double motorR = 0;
            
            double offset = (blackV + whiteV) / 2;


            error = (double)Sensors[2] - offset;
            integral = (2/3) * integral + error;
            turn = Kp * error + Ki * integral;

            motorL = RobotSettings.Instance.MaxSpeed / 2 + turn;
            motorR = RobotSettings.Instance.MaxSpeed / 2 - turn;

            if (motorL < 0)
                motorL = motorL * -1;
            if (motorR < 0)
                motorR = motorR * -1;


            return new Tuple<double, double>(motorL, motorR);
        }

        private Tuple<double, double> sixthVariant()    //PID
        {
            double motorL = 0;
            double motorR = 0;

            Kp = 0.5 * Kc;
            Ki = 0.1 * (2 * Kp * dt) / Pc;
            Kd = 1 * (Kp * Pc) / (8 * dt);
            //Ki = 0;
            //Kd = 0;

            double offset = (blackV + whiteV) / 2;
            

            error = (double)Sensors[2] - offset;
            integral = (2 / 3) * integral + error;
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
