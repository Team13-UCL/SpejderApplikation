using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SpejderApplikation.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Hvis tekstboksen er tom, vis placeholder, ellers skjul den
            EmailPlaceholder.Visibility = string.IsNullOrEmpty(EmailTextBox.Text)
                ? Visibility.Visible
                : Visibility.Hidden;
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Viser adgangskodefeltet er tomt, vis placeholder, ellers skjuler den boxsen
            PasswordPlaceholder.Visibility = string.IsNullOrEmpty(PasswordBox.Password)
                ? Visibility.Visible
                : Visibility.Hidden;
        }
    }
}
