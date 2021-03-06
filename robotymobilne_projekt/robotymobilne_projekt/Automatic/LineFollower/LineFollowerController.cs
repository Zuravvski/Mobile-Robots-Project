﻿using System;
using System.Collections.ObjectModel;
using robotymobilne_projekt.Manual;

namespace robotymobilne_projekt.Automatic.LineFollower
{
    public class LineFollowerController : AbstractController
    {
        private ObservableCollection<int> sensors;
        private LineFollowerAlgorithm algorithm;

        #region Setters & Getters
        public ObservableCollection<int> Sensors
        {
            private get { return sensors; }
            set
            {
                if (value == null) return;
                sensors = value;
                NotifyPropertyChanged("Sensors");
            }
        }

        public LineFollowerAlgorithm Algorithm
        {
            get
            {
                return algorithm;
            }
            set
            {
                algorithm = value;
                NotifyPropertyChanged("Algorithm");
            }
        }

        #endregion

        public override Tuple<double, double> execute()
        {
            return algorithm.execute(sensors);
        }
    }
}
