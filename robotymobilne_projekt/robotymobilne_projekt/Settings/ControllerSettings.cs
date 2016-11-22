using MobileRobots.Manual;
using OpenTK.Input;
using robotymobilne_projekt.Manual;
using robotymobilne_projekt.Utils;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace robotymobilne_projekt.Settings
{
    public class ControllerSettings : ObservableObject
    {
        private static readonly Lazy<ControllerSettings> instance = new Lazy<ControllerSettings>(() => new ControllerSettings());
        private ObservableCollection<AbstractController> controllers;
        private Thread scanningThread;
        private int latency;
        private int scanTime;

        private readonly object scanMutex = new object();

        #region Default values
        private const int defaultLatency = 100;
        private const int defaultScanTime = 3000; // 3s
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
            scanTime = defaultScanTime;
            controllers = new ObservableCollection<AbstractController>();
            scanningThread = new Thread(scanPads);
            scanningThread.Start();
        }

        public static ControllerSettings Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private void scanPads()
        {
            while (true)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (Joystick.GetState(i).IsConnected)
                    {
                        AbstractController newGamepad = new GamepadController(i);
                        if (!controllers.Contains(newGamepad))
                        {
                            lock (scanMutex)
                            {
                                Controllers.Add(newGamepad);
                            }
                        }
                    }
                }
                Thread.Sleep(scanTime);
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
        }
    }
}
