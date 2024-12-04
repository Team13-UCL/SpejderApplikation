using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpejderApplikation.MVVM;
using SpejderApplikation.Model;
using SpejderApplikation.Repository;


namespace SpejderApplikation.ViewModel
{
    class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(IRepository<User> repository)
        {
            
            _users = new List<User>(repository.GetAll());
        }

        
        private List<User> _users;
        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        private User _authenticatedUser;
        public User AuthenticatedUser
        {
            get { return _authenticatedUser; }
            private set
            {
                _authenticatedUser = value;
                OnPropertyChanged();
            }
        }

        public void Login(string username, string password)
        {
            User User = null;
            foreach (var user in _users)
                if (username == user.UserName)
                    if (password == user.Password)
                    {
                        User = user;
                        break;
                    }

            AuthenticatedUser = User;
        }
        public bool Authenticate()
        {
            bool authentication = false;
            Login(Username, Password);
            return AuthenticatedUser != null;
        }
        private bool filled()
        {
            if (Password != null && Username != null) { return true; }
            else return false;
        }
        public RelayCommand LoginCommand => new RelayCommand(execute => Authenticate(), CanExecute => filled() == true);

    }
}

