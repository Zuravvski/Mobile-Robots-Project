using robotymobilne_projekt.GUI.Views.Manual;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace robotymobilne_projekt.GUI.ViewModels
{
    public class ManualViewModel : ViewModel
    {
        private ICommand addUser;
        private ICommand removeUser;

        #region Setters & Getters
        public int Rows { set; get; }
        public int Cols { set; get; }
        public ObservableCollection<UserInterface> Users { get; }
        #endregion

        public ManualViewModel()
        {
            Users = new ObservableCollection<UserInterface>();
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
                            var newUser = new UserInterface {DataContext = new RobotViewModel(this)};
                            Users.Add(newUser);
                            manageLayout();
                        }
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
