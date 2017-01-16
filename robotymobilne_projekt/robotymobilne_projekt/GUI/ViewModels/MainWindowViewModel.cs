using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace robotymobilne_projekt.GUI.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private Geometry logo;

        public Geometry Logo
        {
            set
            {
                logo = value;
                NotifyPropertyChanged("Logo");
            }
            get
            {
                return logo;
            }
        }


        public MainWindowViewModel()
        {
            //default robot icon
            Logo = Geometry.Parse("M0 640 l0 -640 640 0 640 0 0 640 0 640 -640 0 -640 0 0 -640z m440 229 l0 -110 -37 3 -38 3 -3 108 -3 107 41 0 40 0 0 -111z m350 1 l0 -110 -155 0 -155 0 0 110 0 110 155 0 155 0 0 -110z m120 0 l0 -110 -35 0 -35 0 0 110 0 110 35 0 35 0 0 -110z m-80 -194 c0 -24 8 -58 17 -74 16 -28 16 -31 0 -55 -9 -15 -17 -42 -17 -62 l0 -35 -90 0 -90 0 0 -38 c0 -21 5 -43 11 -49 6 -6 9 -21 7 -34 -4 -29 -39 -37 -58 -15 -10 13 -9 20 4 41 9 13 16 40 16 60 l0 35 -90 0 -90 0 0 33 c0 19 -7 48 -16 66 -14 29 -14 34 0 56 9 13 16 45 16 70 l0 45 190 0 190 0 0 -44z                   M567 923 c-12 -12 -7 -80 7 -92 27 -23 46 -2 46 50 0 45 -2 49 -23 49 -13 0 -27 -3 -30 -7z                   M657 923 c-12 -12 -7 -80 7 -92 27 -23 46 -2 46 50 0 45 -2 49 -23 49 -13 0 -27 -3 -30 -7z                   M600 660 c0 -5 20 -10 45 -10 25 0 45 5 45 10 0 6 -20 10 -45 10 -25 0 -45 -4 -45 -10z                   M532 608 c-19 -19 -14 -56 8 -63 24 -8 50 10 50 34 0 19 -19 41 -35 41 -6 0 -16 -5 -23 -12z                   M702 608 c-25 -25 -6 -68 30 -68 9 0 22 9 28 19 13 25 13 27 -6 45 -19 19 -36 20 -52 4z");
        }
    }
}
