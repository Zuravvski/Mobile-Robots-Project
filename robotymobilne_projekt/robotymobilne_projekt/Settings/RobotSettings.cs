using MobileRobots;
using System;
using System.Collections.Generic;

namespace robotymobilne_projekt.Settings
{
    class RobotSettings
    {
        // Singleton
        private static Lazy<RobotSettings> instance = new Lazy<RobotSettings>(() => new RobotSettings());

        // setting fields
        private bool useLights;
        private double maxSpeed;
        private double steeringSensivity;
        private double nitroValue;

        private List<RobotModel> robots;

        // setters and getters
        public double MAX_SPEED
        {
            get
            {
                return maxSpeed;
            }
            set
            {
                maxSpeed = value;
            }
        }
        public double STEERING_SENSIVITY
        {
            get
            {
                return steeringSensivity;
            }
            set
            {
                steeringSensivity = value;
            }
        }
        public double NITRO_VALUE
        {
            get
            {
                return nitroValue;
            }
            set
            {
                nitroValue = value;
            }
        }
        public bool USE_LIGHTS
        {
            get
            {
                return useLights;
            }
            set
            {
                useLights = value;
            }
        }

        private RobotSettings()
        {
            // default values
            useLights = false;
            maxSpeed = 80;
            nitroValue = 1.2;
            steeringSensivity = 35;
        }

        public static RobotSettings INSTANCE
        {
            get
            {
                return instance.Value;
            }
        }

        // TODO: To be implemented
        public List<RobotModel> AVAILABLE_ROBOTS
        {
            get
            {
                return robots;
            }
        }
    }
}
