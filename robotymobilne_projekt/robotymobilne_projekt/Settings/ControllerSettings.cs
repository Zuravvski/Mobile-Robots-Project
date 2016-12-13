using OpenTK.Input;
using robotymobilne_projekt.Manual;
using System;
using System.Threading;
using robotymobilne_projekt.Utils;

namespace robotymobilne_projekt.Settings
{
    public class ControllerSettings : IDisposable
    {
        private static readonly Lazy<ControllerSettings> instance = new Lazy<ControllerSettings>(() => new ControllerSettings());
        private readonly Thread scanningThread;
        private readonly int latency;
        private readonly int scanTime;

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
        }
        public AsyncObservableCollection<AbstractController> Controllers { get; }
        #endregion

        private ControllerSettings()
        {
            latency = defaultLatency;
            scanTime = defaultScanTime;
            Controllers = new AsyncObservableCollection<AbstractController>();
            scanningThread = new Thread(scanPads) {IsBackground = true};
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
                    AbstractController newGamepad = new GamepadController(i);
                    if (Joystick.GetState(i).IsConnected)
                    {
                        if (!Controllers.Contains(newGamepad))
                        {
                            Controllers.Add(newGamepad);
                        } 
                    }
                    else
                    {
                        if(Controllers.Contains(newGamepad))
                        {
                            Controllers.Remove(newGamepad);
                        }
                    }
                }
                Thread.Sleep(scanTime);
            }
        }

        private void addDefaultKeyboardControllers()
        {
            Controllers.Add(new NullObjectController("NONE"));
            Controllers.Add(new KeyboardController(0, Keyboard.GetState(), Key.Up, Key.Down, Key.Left,
                Key.Right, Key.Comma, Key.Period, Key.RShift, Key.Space));
            Controllers.Add(new KeyboardController(1, Keyboard.GetState(), Key.W, Key.S, Key.A, Key.D,
                Key.T, Key.Y, Key.LShift, Key.H));
        }

        public void initialize()
        {
            addDefaultKeyboardControllers();
        }

        public void Dispose()
        {
            scanningThread.Abort();
        }
    }
}
