using robotymobilne_projekt.Settings;

namespace robotymobilne_projekt.GUI.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        public ApplicationService AppService => ApplicationService.Instance;
    }
}
