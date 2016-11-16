using robotymobilne_projekt.GUI.Views;
using System.Collections.Generic;

namespace robotymobilne_projekt.GUI.ViewModels
{
    class ManualViewModel
    {
        public int ROWS { set; get; }
        public int COLS { set; get; }
        public List<UserInterface> USERS { set; get; }

        public ManualViewModel()
        {
            USERS = new List<UserInterface>(4); // max capacity is 4
            addUser();
        }

        private void addUser()
        {
            UserInterface firstUser = new UserInterface();
            firstUser.DataContext = new RobotViewModel();
            USERS.Add(firstUser);
        }

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
    }
}
