using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SpejderApplikation.ViewModel;

namespace SpejderApplikation.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        LoginViewModel vm;
        public Login()
        {
            var UserRepo = new Repository.UserRepo();
            vm = new LoginViewModel(UserRepo);
            InitializeComponent();
            DataContext = vm;
        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Hvis tekstboksen er tom, vis placeholder, ellers skjul den
            EmailPlaceholder.Visibility = string.IsNullOrEmpty(EmailTextBox.Text)
                ? Visibility.Visible
                : Visibility.Hidden;
        }
        private void PasswordTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            // Viser adgangskodefeltet er tomt, vis placeholder, ellers skjuler den boxsen
            PasswordPlaceholder.Visibility = string.IsNullOrEmpty(PasswordBox.Text)
                ? Visibility.Visible
                : Visibility.Hidden;
        }
               

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (vm.Authenticate() == true)
            {
                MessageBox.Show("Login Success");
                ScoutsProgramView scoutsProgramView = new ScoutsProgramView();
                scoutsProgramView.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Login Failed");
            }
        }
    }
}
