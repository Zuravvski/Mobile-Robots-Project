using MobileRobots.Manual;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace robotymobilne_projekt.Settings
{
    class ControllerSettings
    {
        private static readonly Lazy<ControllerSettings> instance = new Lazy<ControllerSettings>(() => new ControllerSettings());
        private int latency;
        private Dictionary<AbstractController, bool> controllers;
        private static object syncRoot = new object();

        // setters and getters
        public int LATENCY
        {
            get
            {
                return latency;
            }
            set
            {
                latency = value;
            }
        }

        private ControllerSettings()
        {
            // Default value
            latency = 100;
            controllers = new Dictionary<AbstractController, bool>();
        }

        public static ControllerSettings INSTANCE
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
                    if (!controllers.ContainsKey(newGamepad))
                    {
                        controllers.Add(newGamepad, false);
                    }
                }
            }
        }

        public void addDefaultKeyboardControllers()
        {
            controllers.Add(new KeyboardController(0, Keyboard.GetState(), Key.Up, Key.Down, Key.Left,
                Key.Right, Key.Comma, Key.Period, Key.RShift, Key.Space), false);
            controllers.Add(new KeyboardController(1, Keyboard.GetState(), Key.W, Key.S, Key.A, Key.D,
                Key.T, Key.Y, Key.LShift, Key.H), false);
        }

        public void reserveController(AbstractController controllerToReserve)
        {
            if(controllers.ContainsKey(controllerToReserve))
            {
                controllers[controllerToReserve] = true;
            }
        }

        public void freeController(AbstractController controllerToFree)
        {
            if (controllers.ContainsKey(controllerToFree))
            {
                controllers[controllerToFree] = false;
            }
        }

        public List<AbstractController> AVAILABLE_CONTROLLERS
        {
            get
            {
                return controllers.Where(pred => !pred.Value)
                                  .Select(pred => pred.Key)
                                  .ToList();
            }
        }

        public void initialize()
        {
            addDefaultKeyboardControllers();
            scanPads();
        }
    }
}
