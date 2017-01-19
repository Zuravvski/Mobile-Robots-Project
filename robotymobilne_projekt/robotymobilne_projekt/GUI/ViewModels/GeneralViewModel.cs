using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using robotymobilne_projekt.Settings;

namespace robotymobilne_projekt.GUI.ViewModels
{
    class GeneralViewModel : ViewModel
    {
        private ICommand save;
         
        public string Ip
        {
            get { return ApplicationSettings.Instance.Ip; }
            set
            {
                ApplicationSettings.Instance.Ip = value;
                NotifyPropertyChanged("Ip");
            }
        }

        public int Port
        {
            get { return ApplicationSettings.Instance.Port; }
            set
            {
                ApplicationSettings.Instance.Port = value;
                NotifyPropertyChanged("Port");
            }
        }

        public ApplicationSettings.ApplicationMode AppMode
        {
            get { return ApplicationSettings.Instance.AppMode; }
            set
            {
                ApplicationSettings.Instance.AppMode = value;
                NotifyPropertyChanged("AppMode");
            }
        }

        public ICommand Save
        {
            get
            {
                return save ?? (save = new DelegateCommand(delegate
                {
                    ApplicationSettings.Instance.save();
                }));
            }
        }
    }
}
