using System.Windows.Input;
using robotymobilne_projekt.Settings;

namespace robotymobilne_projekt.GUI.ViewModels
{
    class GeneralViewModel : ViewModel
    {
        private ICommand save;
         
        public string Ip
        {
            get { return ApplicationService.Instance.Ip; }
            set
            {
                ApplicationService.Instance.Ip = value;
                NotifyPropertyChanged("Ip");
            }
        }

        public int Port
        {
            get { return ApplicationService.Instance.Port; }
            set
            {
                ApplicationService.Instance.Port = value;
                NotifyPropertyChanged("Port");
            }
        }

        public ApplicationService.ApplicationMode AppMode
        {
            get { return ApplicationService.Instance.AppMode; }
        }

        public ICommand Save
        {
            get
            {
                return save ?? (save = new DelegateCommand(delegate
                {
                    ApplicationService.Instance.save();
                }));
            }
        }
    }
}
