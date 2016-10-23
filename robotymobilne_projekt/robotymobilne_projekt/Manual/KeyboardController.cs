using robotymobilne_projekt.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotymobilne_projekt.Manual
{
    class KeyboardController : IController
    {
        private RobotModel robot;  // Robot assigned to controller
        private MainWindow window; // window that reacts to key press

        private byte speed;
        private const double turningRatio = 0.8;

        // setters and getters
        public RobotModel ROBOT
        {
            get
            {
                return robot;
            }
            set
            {
                robot = value;
            }
        }

        public KeyboardController(MainWindow window)
        {
            this.window = window;
            window.KeyDown += Window_KeyDown;

            // testing hardcoded values
            speed = 20;
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.W:
                    robot.DIRECTION = RobotModel.MOVE_DIRECTION.FORWARD;
                    calculateFrame(speed, speed);
                    break;

                case System.Windows.Input.Key.A:
                    calculateFrame((byte)(speed * turningRatio), speed);
                    break;

                case System.Windows.Input.Key.S:
                    robot.DIRECTION = RobotModel.MOVE_DIRECTION.BACKWARD;
                    calculateFrame(speed, speed);
                    break;

                case System.Windows.Input.Key.D:
                    calculateFrame(speed, (byte)(speed * turningRatio));
                    break;

                case System.Windows.Input.Key.Space:
                    robot.DIRECTION = RobotModel.MOVE_DIRECTION.IDLE;
                    calculateFrame(speed, speed);
                    break;

                case System.Windows.Input.Key.Add:
                    if (speed <= 127)
                    {
                        speed++;
                    }
                    break;

                case System.Windows.Input.Key.Subtract:
                    if (speed >= 0)
                    { 
                        speed--;
                    }
                    break;
                
            }
        }

        private void calculateFrame(byte v1, byte v2)
        {
            string calculatedFrame = "03";
            string hexV1;
            string hexV2;
            switch (robot.DIRECTION)
            { 
                case RobotModel.MOVE_DIRECTION.FORWARD:
                    hexV1 = v1.ToString("X2");
                    hexV2 = v2.ToString("X2");
                    calculatedFrame += hexV1 + hexV2;
                    break;

                case RobotModel.MOVE_DIRECTION.BACKWARD:
                    hexV1 = (-v1).ToString("X2").Substring(6);
                    hexV2 = (-v2).ToString("X2").Substring(6);
                    calculatedFrame += hexV1 + hexV2;
                    break;

                case RobotModel.MOVE_DIRECTION.IDLE:
                default:
                    calculatedFrame += "0000";
                    break;
            }
            execute(calculatedFrame);
        }

        public void execute(string action)
        {
            try
            {
                robot.sendDataFrame(action);
            }
            catch (Exception ex)
            {
                Logger.getLogger().log("The controller is not attached to any robot", ex);
            }
        }
    }
}
