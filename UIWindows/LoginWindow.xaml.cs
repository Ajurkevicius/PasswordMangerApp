using ImageRecognition.Data;
using ImageRecognition.Encryption;
using ImageRecognition.Repository;
using ImageRecognition.State;
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

namespace ImageRecognition.UIWindows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            string loginName = LoginName.Text;
            string passwordName = LoginPassword.Password;

            User userToCheck = UserRepository.GetUserWithAccounts(loginName);
            if(userToCheck == null)
            {
                MessageBox.Show("Wrong credentials");
            }
            else
            {
                bool isUserValid = Hasher.Validate(userToCheck.MasterPasswrod, passwordName);
                if(isUserValid == true)
                {
                    CurrentUser.ChangeStatusToActive(userToCheck);
                    CurrentUser.SetCurrentUserPassword(passwordName);
                    MainMenuWindow mainMenuWindow = new MainMenuWindow();
                    mainMenuWindow.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong credentials");
                }
            }
        }
    }
}
