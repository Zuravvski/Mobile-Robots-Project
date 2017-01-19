using robotymobilne_projekt.Settings;

namespace robotymobilne_projekt.GUI.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        public ApplicationService.ApplicationMode AppMode
        {
            get { return ApplicationService.Instance.AppMode; }
        }
    }
}
