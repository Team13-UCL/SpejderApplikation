using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpejderApplikation.MVVM;
using SpejderApplikation.Model;
using SpejderApplikation.Repository;

// LoginViewModel is responsible for handling user authentication and login functionality
namespace SpejderApplikation.ViewModel
{
    class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(IRepository<User> repository)
        {

            _users = new List<User>(repository.GetAll());
        }


        private List<User> _users; // List to store users fetched from the repository
        private string _username; // Stores the input username

        // Username property with notification on change
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        private string _password; // Stores the input password

        // Password property with notification on change
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        private User _authenticatedUser; // Stores the authenticated user after login

        // AuthenticatedUser property with notification on change
        public User AuthenticatedUser
        {
            get { return _authenticatedUser; }
            private set
            {
                _authenticatedUser = value;
                OnPropertyChanged();
            }
        }

        // Method to authenticate the user based on username and password
        public void Login(string username, string password)
        {
            User User = null; // Placeholder for matched user
            foreach (var user in _users)
                if (username == user.UserName)
                    if (password == user.Password)
                    {
                        User = user;
                        break;
                    }

            AuthenticatedUser = User; // Set the authenticated user
        }

        // Method to check if authentication is successful
        public bool Authenticate()
        {            
            Login(Username, Password); // Perform login
            return AuthenticatedUser != null; // Return true if user is authenticated
        }

        // Helper method to check if username and password fields are filled
        private bool filled()
        {
            if (Password != null && Username != null) { return true; }
            else return false;
        }

        // RelayCommand to bind the login functionality to a UI element
        public RelayCommand LoginCommand => new RelayCommand(execute => Authenticate(), CanExecute => filled() == true);

    }
}

