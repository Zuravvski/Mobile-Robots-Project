using MobileRobots.Manual;
using OpenTK.Input;
using robotymobilne_projekt.Manual;
using robotymobilne_projekt.Utils;
using System;
using System.Collections.ObjectModel;

namespace robotymobilne_projekt.Settings
{
    public class ControllerSettings : ObservableObject
    {
        private static readonly Lazy<ControllerSettings> instance = new Lazy<ControllerSettings>(() => new ControllerSettings());
        private ObservableCollection<AbstractController> controllers;
        private int latency;

        #region Default values
        private const int defaultLatency = 100;
        #endregion

        #region Getters & Setters
        public int Latency
        {
            get
            {
                return latency;
            }
            set
            {
                latency = value;
                NotifyPropertyChanged("Latency");
            }
        }
        public ObservableCollection<AbstractController> Controllers
        {
            get
            {
                return controllers;
            }
        }
        #endregion

        private ControllerSettings()
        {
            latency = defaultLatency;
            controllers = new ObservableCollection<AbstractController>();
        }

        public static ControllerSettings Instance
        {
            get
            {
                return instance.Value;
            }
        }

        // TODO: Test this
        public void scanPads()
        {
            for (int i = 0; i < 4; i++)
            {
                if (Joystick.GetState(i).IsConnected)
                {
                    AbstractController newGamepad = new GamepadController(i);
                    if (controllers.Contains(newGamepad))
                    {
                        controllers.Add(newGamepad);
                    }
                }
            }
        }

        public void addDefaultKeyboardControllers()
        {
            controllers.Add(new NullObjectController("NONE"));
            controllers.Add(new KeyboardController(0, Keyboard.GetState(), Key.Up, Key.Down, Key.Left,
                Key.Right, Key.Comma, Key.Period, Key.RShift, Key.Space));
            controllers.Add(new KeyboardController(1, Keyboard.GetState(), Key.W, Key.S, Key.A, Key.D,
                Key.T, Key.Y, Key.LShift, Key.H));
        }

        public void reserveController(AbstractController controllerToReserve)
        {
            controllers.Remove(controllerToReserve);
        }

        public void freeController(AbstractController controllerToFree)
        {
            controllers.Add(controllerToFree);
        }

        public void initialize()
        {
            addDefaultKeyboardControllers();
            scanPads();
        }
    }
}
