using robotymobilne_projekt.GUI.Views.Manual;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace robotymobilne_projekt.GUI.ViewModels.Manual
{
    public class ManualViewModel : ViewModel
    {
        private ICommand addUser;
        private ICommand removeUser;
        private System.Windows.Visibility startInfoVisibility;

        #region Setters & Getters
        public System.Windows.Visibility StartInfoVisibility
        {
            set
            {
                startInfoVisibility = value;
                NotifyPropertyChanged("StartInfoVisibility");
            }
            get
            {
                return startInfoVisibility;
            }
        }
        public int Rows { set; get; }
        public int Cols { set; get; }
        public ObservableCollection<UserInterface> Users { get; }
        #endregion

        public ManualViewModel()
        {
            Users = new ObservableCollection<UserInterface>();
            StartInfoVisibility = System.Windows.Visibility.Visible;
        }

        #region Actions
        public ICommand AddUser
        {
            get
            {
                if (null == addUser)
                {
                    addUser = new DelegateCommand(delegate ()
                    {
                        if (Users.Count < 4)
                        {
                            var newUser = new UserInterface {DataContext = new UserViewModel(this)};
                            Users.Add(newUser);
                            manageLayout();
                        }

                        StartInfoVisibility = System.Windows.Visibility.Hidden;
                    });
                }
                return addUser;
            }
        }
        public ICommand RemoveUser
        {
            get
            {
                if(null == removeUser)
                {
                    removeUser = new DelegateCommand<RobotViewModel>(delegate (RobotViewModel userVM)
                    {
                        for(int i = 0; i < Users.Count; i++)
                        {
                            if(Users[i].DataContext.Equals(userVM))
                            {
                                Users.Remove(Users[i]);
                                manageLayout();
                            }
                        }

                        if (Users.Count == 0)
                        {
                            StartInfoVisibility = System.Windows.Visibility.Visible;
                        }
                    });
                }
                return removeUser;
            }
        }
        #endregion

        #region Helper Methods
        private void manageLayout()
        {
            switch (Users.Count)
            {
                case 1:
                    Rows = 1;
                    Cols = 1;
                    break;

                case 2:
                    Rows = 1;
                    Cols = 2;
                    break;

                case 3:
                case 4:
                    Rows = 2;
                    Cols = 2;
                    break;

                default:
                    Rows = 0;
                    Cols = 0;
                    break;
            }
        }
        #endregion
    }
}
