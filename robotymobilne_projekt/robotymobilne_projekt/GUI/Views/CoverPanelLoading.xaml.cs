using FirstFloor.ModernUI.Presentation;
using robotymobilne_projekt.GUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace robotymobilne_projekt.GUI.Views
{
    /// <summary>
    /// Interaction logic for CoverPanelLoading.xaml
    /// </summary>
    public partial class CoverPanelLoading : UserControl
    {
        public static AppearanceViewModel viewModel
        {
            set
            {
                
            }
        }

        public CoverPanelLoading()
        {
            InitializeComponent();
        }
    }
}
