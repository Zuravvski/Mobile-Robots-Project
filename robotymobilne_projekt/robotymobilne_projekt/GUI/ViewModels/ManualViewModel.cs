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
        public int ROWS { set; get; }
        public int COLS { set; get; }
        public ObservableCollection<UserInterface> USERS { get; }
        #endregion

        public ManualViewModel()
        {
            USERS = new ObservableCollection<UserInterface>();
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
                        if (USERS.Count < 4)
                        {
                            UserInterface newUser = new UserInterface();
                            newUser.DataContext = new RobotViewModel();
                            USERS.Add(newUser);
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
                    removeUser = new DelegateCommand<UserInterface>(delegate (UserInterface user)
                    {
                        USERS.Remove(user);
                    });
                }
                return removeUser;
            }
        }
        #endregion

        #region Helper Methods
        private void manageLayout()
        {
            switch (USERS.Count)
            {
                case 1:
                    ROWS = 1;
                    COLS = 1;
                    break;

                case 2:
                    ROWS = 1;
                    COLS = 2;
                    break;

                case 3:
                case 4:
                    ROWS = 2;
                    COLS = 2;
                    break;

                default:
                    ROWS = 0;
                    COLS = 0;
                    break;
            }
        }
        #endregion
    }
}
