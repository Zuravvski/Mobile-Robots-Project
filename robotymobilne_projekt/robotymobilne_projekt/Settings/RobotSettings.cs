using robotymobilne_projekt.Devices;
using robotymobilne_projekt.Utils;
using robotymobilne_projekt.Utils.AppLogger;
using System;
using System.Collections.ObjectModel;

namespace robotymobilne_projekt.Settings
{
    public class RobotSettings : ObservableObject
    {
        private static readonly Lazy<RobotSettings> instance = new Lazy<RobotSettings>(() => new RobotSettings());
        private bool useLights;
        private double maxSpeed;
        private double steeringSensivity;
        private double nitroFactor;
        private int reconnectAttempts;

        #region Default values
        private const bool defaultUseLights = false;
        private const double defaultMaxSpeed = 20;
        private const double defaultSteeringSensibity = 15;
        private const double defaultNitroFactor = 1.1;
        private const int defaultReconnectAttempts = 3;
        #endregion
        #region Constants
        public const string noLights = "00";
        public const string leftLight = "01";
        public const string rightLight = "02";
        public const string bothLights = "03";
        #endregion

        private readonly ObservableCollection<RobotModel> robots;

        #region Setters & Getters
        public double MaxSpeed
        {
            get
            {
                return maxSpeed;
            }
            set
            {
                maxSpeed = value;
                NotifyPropertyChanged("MaxSpeed");
            }
        }
        public double SteeringSensivity
        {
            get
            {
                return steeringSensivity;
            }
            set
            {
                steeringSensivity = value;
                NotifyPropertyChanged("SteeringSensivity");
            }
        }
        public double NitroFactor
        {
            get
            {
                return nitroFactor;
            }
            set
            {
                nitroFactor = value;
                NotifyPropertyChanged("NitroFactor");
            }
        }
        public bool UseLights
        {
            get
            {
                return useLights;
            }
            set
            {
                useLights = value;
                NotifyPropertyChanged("UseLights");
            }
        }
        public int ReconnectAttempts
        {
            get
            {
                return reconnectAttempts;
            }
            set
            {
                if (value >= 0 && value <= 10)
                {
                    reconnectAttempts = value;
                }
                NotifyPropertyChanged("ReconnectAttempts");
            }
        }
        public ObservableCollection<RobotModel> Robots
        {
            get
            {
                return robots;
            }
        }
        #endregion

        private RobotSettings()
        {
            robots = new ObservableCollection<RobotModel>();

            // setting default values
            reset();
        }

        public void reset()
        {
            UseLights = defaultUseLights;
            MaxSpeed = defaultMaxSpeed;
            NitroFactor = defaultNitroFactor;
            SteeringSensivity = defaultSteeringSensibity;
            ReconnectAttempts = defaultReconnectAttempts;
        }

        public void initialize()
        {
            if(0 == robots.Count)
            {
                robots.Add(new NullObjectRobot(-1, "", 0));
                //robots.Add(new RobotModel(30, "127.0.0.1", 50131));
                for (var i = 0; i < 6; i++)
                {
                    var newRobot = new RobotModel(i + 30, "192.168.2.3" + i, 8000);
                    robots.Add(newRobot);
                }
            }
            else
            {
                Logger.Instance.log(LogLevel.INFO, "Initialization can be called only once");
            }
        }

        public static RobotSettings Instance
        {
            get
            {
                return instance.Value;
            }
        }
    }
}
