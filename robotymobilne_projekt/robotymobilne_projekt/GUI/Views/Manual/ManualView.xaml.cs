﻿using robotymobilne_projekt.GUI.ViewModels;
using System.Windows.Controls;
using robotymobilne_projekt.GUI.ViewModels.Manual;

namespace robotymobilne_projekt.GUI.Views.Manual
{
    /// <summary>
    /// Interaction logic for ManuaView.xaml
    /// </summary>
    public partial class ManualView : Page
    {
        public ManualView()
        {
            InitializeComponent();
            DataContext = new ManualViewModel();
        }
    }
}
